using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.model.BS
{
    public class Statistic
    {
        public DateTime LastDatePayment { get; set; }
        public DateTime LastChangePlan { get; set; }
        public double Balans { get;  set; }
        public double OutcomingMinutes { get; set; }
        public double IncomingMinutes { get; set; } 
        public double TotalCost { get; set; }
    }
}
