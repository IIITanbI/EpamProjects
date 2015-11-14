﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3
{
    class Request
    {
        public const int IncomingCall = 101;
        public const int OutcomingCall = 102;
        public const int DisconnectCall = 103;
        public Request(PhoneNumber source, PhoneNumber target, int code)
        {
            this.Sourse = source;
            this.Target = target;
            this.Code = code;
        }

        public int Code { get; }

        public PhoneNumber Sourse { get; }
        public PhoneNumber Target { get;}

        public override string ToString()
        {
            string res = "Request:";
            res += " Source: " + Sourse;
            res += " Target: " + Target;
            res += " Code: " + Code;
            return res;
        }
    }

    
}
