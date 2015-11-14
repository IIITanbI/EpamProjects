using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    enum RequestType
    {
        IncomCall,
        OutcomCall,
        DisconnectCall
    }
    class Request
    {
        public RequestType RequestType { get; set; }
        public PhoneNumber Sourse { get; set; }
        public PhoneNumber Target { get; set; }

    }

    
}
