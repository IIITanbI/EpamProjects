using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Project3.model.BS
{
    public class CallStatistic
    {
        public CallInfo CallInfo { get; }
        public double Cost { get;  }
        public CallStatistic(CallInfo callInfo, double cost)
        {
            CallInfo = callInfo;
            Cost = cost;
        }
    }
}
