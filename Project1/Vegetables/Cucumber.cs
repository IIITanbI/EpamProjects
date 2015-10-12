using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    class Cucumber : Vegetable
    {
        public Cucumber(double weight) 
            : base("Cucumber", weight, new Calories(0.8, 0.1, 2.8))
        {
        }
    }
}
