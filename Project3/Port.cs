using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Port
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

        public void RegisterEventsForTerminal(Terminal terminal)
        {
            terminal.Plugging += (sender, args) =>
            {
                Console.WriteLine((sender as Terminal)?.PhoneNumber + " plugging");
            };
        }
        public void ClearEvents(Terminal terminal)
        {
            this.StateChanged = null;
            this.StateChanging = null;
        }

        public event EventHandler<PortState> StateChanging;
        public event EventHandler<PortState> StateChanged;
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
