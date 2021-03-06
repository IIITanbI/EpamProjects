﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public Cucumber(Cucumber cucumber)
            : base(cucumber)
        {
            
        }

        public override object Clone()
        {
            return new Cucumber(this);
        }
    }
}
