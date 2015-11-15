using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Interfaces
{
    public interface IPort: IClearEvents
    {
        PortState State { get; set; }

        event EventHandler<PortState> StateChanging;
        event EventHandler<PortState> StateChanged;

        void RegisterEventsForTerminal(ITerminal terminal);
    }
}
