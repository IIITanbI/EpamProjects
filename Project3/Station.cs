using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Interfaces;

namespace Project3
{
    class Station
    {
        private ICollection<Port> _ports;
        private ICollection<Terminal> _terminals;

        private IDictionary<Terminal, Port> _portMap; 
        public Station()
        {
            this._ports = new List<Port>();
            this._terminals = new List<Terminal>();
            this._portMap = new Dictionary<Terminal, Port>();
        }
        public Station(ICollection<Port> ports, ICollection<Terminal> terminals)
        {
            this._ports = ports;
            this._terminals = terminals;
            this._portMap = new Dictionary<Terminal, Port>();
        }

        public void MapPort(Terminal terminal, Port port)
        {
            if (port == null)
                throw new ArgumentNullException(nameof(port) + " is null");
            if (terminal == null)
                throw new ArgumentNullException(nameof(terminal) + " is null");
            if (this._portMap.Values.Contains(port))
                throw new Exception("This port is already use");
            
            this._portMap.Add(terminal, port);
          
        }

        public Port GetPort(Terminal terminal)
        {
            Port port;
            _portMap.TryGetValue(terminal, out port);
            return port;
        }

        public Terminal GetTerminal(PhoneNumber phoneNumber)
        {
            return this._terminals.FirstOrDefault(terminal => terminal.PhoneNumber == phoneNumber);
        }
        public Port this[Terminal terminal]
        {
            get
            {
                Port port;
                _portMap.TryGetValue(terminal, out port);
                return port;
            }
        }
        
        public void Add(Port port)
        {
            if (!this._ports.Contains(port))
                this._ports.Add(port);
        }
        public void Add(Terminal terminal)
        {
            var freePort = this._ports.Except(_portMap.Values).FirstOrDefault();
            if (freePort == null) return;

            if (this._terminals.Any(term => term.PhoneNumber == terminal.PhoneNumber))
                throw new Exception("this number alredy used");

            if (!this._terminals.Contains(terminal))
                _terminals.Add(terminal);

            MapPort(terminal, freePort);
            freePort.RegisterEventsForTerminal(terminal);
            terminal.RegisterEventForPort(freePort);
            this.RegisterEventForTerminal(terminal);
            this.RegisterEventForPort(freePort);
            freePort.State = PortState.Free;
        }

        protected void RegisterOutgoingRequest(Request request)
        {
            Console.WriteLine("Termianal ocnnect to cstaton");
            if (request.Sourse != default(PhoneNumber) && request.Target != default(PhoneNumber))
            {
                Terminal terminal = GetTerminal(request.Target);
                Port port = GetPort(terminal);

                if (port.State == PortState.Free)
                {
                    port.State = PortState.Busy;
                    terminal.IncomingRequest(request.Sourse);
                }
            }

        }
        protected void InterruptConnection()
        {
            //this._callCollection.Remove(callInfo);
            //SetPortStateWhenConnectionInterrupted(callInfo.Source, callInfo.Target);
            //OnCallInfoPrepared(this, callInfo);
        }

        protected void InterruptActiveCall()
        {
            //callInfo.Duration = DateTime.Now - callInfo.Started;
            //this._callCollection.Remove(callInfo);
            //SetPortStateWhenConnectionInterrupted(callInfo.Source, callInfo.Target);
            //OnCallInfoPrepared(this, callInfo);
        }

        public void OnIncomingCallRespond(object sender, Respond respond)
        {
            Console.WriteLine("Respond ok");
            switch (respond.RespondType)
            {
                case RespondType.Accept:
                    Console.WriteLine("asd");
                    Terminal terminal = sender as Terminal;
                    Port port = GetPort(terminal);
                    port.State = PortState.Busy;
                    break;
                case RespondType.Drop:

                    Console.WriteLine("ad");
                    break;
                default:
                    break;
            }
            //var registeredCallInfo = GetConnectionInfo(respond.Source);
            //if (registeredCallInfo != null)
            //{
            //    switch (respond.State)
            //    {
            //        case Responds.RespondState.Drop:
            //            {
            //                InterruptConnection(registeredCallInfo);
            //                break;
            //            }
            //        case Responds.RespondState.Accept:
            //            {
            //                MakeCallActive(registeredCallInfo);
            //                break;
            //            }
            //    }
            //}
            //else
            //{
            //    CallInfo currentCallInfo = GetCallInfo(respond.Source);
            //    if (currentCallInfo != null)
            //    {
            //        this.InterruptActiveCall(currentCallInfo);
            //    }
            //}
        }
        protected virtual void RegisterEventForTerminal(Terminal terminal)
        {
            terminal.OutConnection += (sender, request) =>
            {
                RegisterOutgoingRequest(request);

            };
            terminal.IncomRespond += OnIncomingCallRespond;


        }
        protected virtual void RegisterEventForPort(Port port)
        {
            port.StateChanged += (sender, state) =>
            {
                Console.WriteLine("station: port changed state to " + port.State);
            };
        }

    }
}
