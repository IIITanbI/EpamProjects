using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    public interface IIngredient : ICloneable
    {
        string Name { get; set; }
        double Weight { get; set; }
        Calories CaloriesPer100G { get; set; }
    }
    public abstract class Ingredient : IIngredient
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public Calories CaloriesPer100G { get; set; }

        protected Ingredient() { }

        protected Ingredient(IIngredient ingredient)
        {
            this.Name = ingredient.Name;
            this.Weight = ingredient.Weight;
            this.CaloriesPer100G = ingredient.CaloriesPer100G;
        }

        protected Ingredient(string name, double weight, Calories caloriesPer100G)
        {
            this.Name = name;
            this.Weight = weight;
            this.CaloriesPer100G = caloriesPer100G;
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
