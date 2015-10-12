using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    public interface IVegetable
    {
        string Name { get; }
        double Weight { get; }
        Calories Calories { get; }
    }

    public class Vegetable : IVegetable
    {
        public string Name { get; }
        public double Weight { get;}
        public Calories Calories { get; }

        protected Vegetable(string name, double weight, Calories calories)
        {
            Name = name;
            Weight = weight;
            Calories = calories;
        }
    }
}
