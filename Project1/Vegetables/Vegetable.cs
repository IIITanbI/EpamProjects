using System;

namespace Project1.Vegetables
{

    public class Vegetable : Ingredient
    {
        public Vegetable() { }
        public Vegetable(string name, double weight, Calories caloriesPer100G)
        {
            this.Name = name;
            this.Weight = weight;
            this.CaloriesPer100G = caloriesPer100G;
        }
        public Vegetable(Vegetable vegetable)
        {
            this.Name = vegetable.Name;
            this.Weight = vegetable.Weight;
            this.CaloriesPer100G = vegetable.CaloriesPer100G;
        }

        public override object Clone()
        {
            return new Vegetable(this);
        }
    }
}
