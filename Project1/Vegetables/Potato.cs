using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    class Potato : Vegetable
    {
        protected Potato(string name, double weight, Calories calories)
            : base(name, weight, calories)
        {
           
        }
        public Potato(double weight) 
            : base("Potato", weight, new Calories(2.0, 0.4, 18.1))
        {
        }
    }

    class FryPotato : Potato
    {

        public FryPotato(double weight)
            : base("Fry Potato", weight, new Calories(2.8, 9.5, 23.4))
        {
        }
    }
}
