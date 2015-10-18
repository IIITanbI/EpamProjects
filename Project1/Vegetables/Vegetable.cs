using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    public interface IVegetable : ICloneable 
    {
        string Name { get; }
        double Weight { get; set; }
        Calories Calories { get; }
    }

    public abstract class Vegetable : IVegetable
    {
        public string Name { get; protected set; }
        public double Weight { get; set; }
        public Calories Calories { get; protected set; }

        protected Vegetable()
        {

        }
        protected Vegetable(string name, double weight, Calories calories)
        {
            this.Name = name;
            this.Weight = weight;
            this.Calories = calories;
        }
        protected Vegetable(IVegetable vegetable)
        {
            this.Name = vegetable.Name;
            this.Weight = vegetable.Weight;
            this.Calories = vegetable.Calories;
        }

        public override string ToString()
        {
            return "Name: " + this.Name + '\n'
                   + "Weight: " + this.Weight + '\n'
                   + "CaloriesPer100G: " + this.Calories + '\n';

        }

        public abstract object Clone();
    }
}
