﻿using System.Runtime.InteropServices;

namespace Project3
{

    class Respond
    {
        public const int Accept = 101;
        public const int Drop = 102;

        public Respond(int code, Request request)
        {
            this.Code = code;
            this.Request = request;
        }
        //public RespondType RespondType { get; set; }
        public int Code { get; }
        public Request Request { get; }

        public override string ToString()
        {
            string res = "Respond:";
            res += " Code: " + this.Code;
            res += " ";
            
            switch (this.Code)
            {
                case Accept:
                    res += "Accepted";
                    break;
                case Drop:
                    res += "Drop";
                    break;
                default:
                    res += "Undfined";
                    break;
            }

            res += " " + Request.ToString();
            return res;
        }
    }
}