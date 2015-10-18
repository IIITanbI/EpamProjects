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
        protected Potato(string name, double weight, Calories caloriesPer100G)
            : base(name, weight, caloriesPer100G)
        {
           
        }
        public Potato(double weight) 
            : base("Potato", weight, new Calories(2.0, 0.4, 18.1))
        {
            this.Name = "asd";
        }

        public new string Name { get; }
        public override object Clone()
        {
            return new Potato(this.Weight);
        }
    }
}
