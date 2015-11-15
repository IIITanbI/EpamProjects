using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Interfaces;

namespace Project3
{
    public class Port : IPort
    {
        private PortState _state = PortState.Off;
        public PortState State
        {
            get
            {
                return _state;
            }
            set
            {
                if (_state == value) return;
                OnStateChanging(this, value);
                _state = value;
                OnStateChanged(this, _state);
            }
        }

        public void ClearEvents()
        {
            this.StateChanged = null;
            this.StateChanging = null;
        }

        public event EventHandler<PortState> StateChanging;
        public event EventHandler<PortState> StateChanged;
        public void RegisterEventsForTerminal(ITerminal terminal)
        {
            terminal.Plugging += (sender, args) => { this.State = PortState.Free; };
            terminal.UnPlugging += (sender, args) => { this.State = PortState.Off; };
            terminal.OutConnection += (sender, request) => 
            {
                if (request.Code == Request.OutcomingCall && this.State == PortState.Free)
                {
                    this.State = PortState.Busy;
                }
            };
        }

        protected virtual void OnStateChanging(object sender, PortState e)
        {
            StateChanging?.Invoke(sender, e);
        }
        protected virtual void OnStateChanged(object sender, PortState e)
        {
            StateChanged?.Invoke(sender, e);
        }

      
    }
}
