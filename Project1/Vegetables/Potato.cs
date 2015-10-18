using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    class Potato : Vegetable
    {
        public Potato(double weight) 
            : base("Potato", weight, new Calories(2.0, 0.4, 18.1))
        {
        }
        public Potato(Potato potato)
            : base(potato)
        {

        }

        public override object Clone()
        {
            return new Potato(this);
        }
    }
}
