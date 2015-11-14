using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Project3.Interfaces;

namespace Project3
{
    class Terminal
    {
        private PhoneNumber _phoneNumber;
        public PhoneNumber PhoneNumber {
            get { return _phoneNumber; }
            set
            {
                this._phoneNumber = value;
                OnPhoneChanged(this, this._phoneNumber);
            }
        }

        public Request Request { get; set; }
        public bool IsOnline { get; private set; }

        public Terminal(PhoneNumber phoneNumber)
        {
            this.PhoneNumber = phoneNumber;
            this.IsOnline = false;
        }

        public event EventHandler<PhoneNumber> PhoneChanged;
        public event EventHandler Plugging;
        public event EventHandler UnPlugging;
        public event EventHandler Online;
        public event EventHandler Offline;

        //terminal connect to station
        public event EventHandler<Request> OutConnection;
        //station connect to terminal
        public event EventHandler<Request> IncomConnection;
        //terminal respond to station
        public event EventHandler<Respond> IncomRespond;


        public void Plug()
        {
            OnPlugging(this, EventArgs.Empty);
        }
        public void UnPlug()
        {
            OnUnPlugging(this, EventArgs.Empty);
        }

        protected virtual void OnPlugging(object sender, EventArgs args)
        {
            Plugging?.Invoke(sender, args);
        }
        protected virtual void OnUnPlugging(object sender, EventArgs args)
        {
            UnPlugging?.Invoke(sender, args);
        }
        protected virtual void OnPhoneChanged(object sender, PhoneNumber e)
        {
            PhoneChanged?.Invoke(sender, e);
        }


        protected virtual void OnOnline(object sender, EventArgs args)
        {
            Online?.Invoke(sender, args);
            this.IsOnline = true;
        }
        protected virtual void OnOffline(object sender, EventArgs args)
        {
            if (IsOnline)
                Offline?.Invoke(sender, args);
            Request = null;
            this.IsOnline = false;
        }

        public void Call(PhoneNumber target)
        {
            if (!IsOnline)
            {
                OnOnline(this, EventArgs.Empty);
                Request request = new Request() {RequestType = RequestType.OutcomCall, Sourse = this.PhoneNumber, Target = target};
                OnOutConnection(this, request);
            }
        }

        protected virtual void OnOutConnection(object sender, Request e)
        {   
            OutConnection?.Invoke(sender, e);
        }
        protected virtual void OnIncomConnection(object sender, Request e)
        {
            IncomConnection?.Invoke(sender, e);
        }
        protected virtual void OnIncomRespond(object sender, Respond e)
        {
            IncomRespond?.Invoke(sender, e);
        }


        public void IncomingRequest(PhoneNumber source)
        {
            Console.WriteLine("cur tel = " + this.PhoneNumber);
            Console.WriteLine("cll from = " + source);

            var Request = new Request() { RequestType = RequestType.IncomCall, Sourse = source, Target = this.PhoneNumber};
            OnIncomConnection(this, Request);
            OnIncomRespond(this, new Respond() {Request = Request, RespondType = RespondType.Accept});
        }
        public void RegisterEventForPort(Port port)
        {
            port.StateChanged += (sender, state) =>
            {
                if (this.IsOnline && (state == PortState.Free || state == PortState.Free))
                {
                    this.OnOffline(this, EventArgs.Empty);
                }
            };
        }


        
    }
}
