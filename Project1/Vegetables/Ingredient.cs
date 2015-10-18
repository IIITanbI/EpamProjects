﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    public interface IVegetable : ICloneable
    {
        string Name { get; set; }
        double Weight { get; set; }
        Calories CaloriesPer100G { get; set; }
    }
    public abstract class Vegetable : IIngredient
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public Calories CaloriesPer100G { get; set; }

        protected Vegetable() { }

        protected Vegetable(IIngredient ingredient)
        {
            Name = ingredient.Name;
            Weight = ingredient.Weight;
            CaloriesPer100G = ingredient.CaloriesPer100G;
        }

        public override string ToString()
        {
            return   "Name: " + this.Name + '\n'
                   + "Weight: " + this.Weight + '\n'
                   + "CaloriesPer100G: " + this.CaloriesPer100G + '\n';

        }

        public abstract object Clone();
    }
}
