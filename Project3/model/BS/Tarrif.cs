using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.model.BS
{
    public class Tarrif
    {
        public string Name { get; }
        public double FreeMinutes { get;}
        public double CostPerMinutes { get; }
        public bool CanHaveNegativeBalanse { get; }
       

        public Tarrif(string name, double freeMinutes, double costPerMinutes, bool canHaveNegativeBalanse)
        {
            Name = name;
            FreeMinutes = freeMinutes;
            CostPerMinutes = costPerMinutes;
            CanHaveNegativeBalanse = canHaveNegativeBalanse;
        }

    }
}
