using System;

namespace Project1.Vegetables
{
    public interface IVegetable : ICloneable 
    {
        string Name { get; }
        double Weight { get; set; }
        Calories Calories { get; }
    }
}