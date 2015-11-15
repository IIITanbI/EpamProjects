using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.Interfaces
{
    public interface IStation: IClearEvents
    {
        event EventHandler<CallInfo> CallInfoAdded; 
        void RegisterEventForTerminal(ITerminal terminal);
        void RegisterEventForPort(IPort port);
    }
}
