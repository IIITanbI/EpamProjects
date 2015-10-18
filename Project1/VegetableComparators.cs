using System.Collections.Generic;
using Project1.Vegetables;

namespace Project1
{
    public static class VegetableComparasions
    {
        public static int CompareByWeight(IVegetable vegetable1, IVegetable vegetable2)
        {
            return vegetable1.Weight.CompareTo(vegetable2.Weight);
        }
        public static int CompareByCalories(IVegetable vegetable1, IVegetable vegetable2)
        {
            return vegetable1.Calories.CompareTo(vegetable2.Calories);
        }
        public static int CompareByName(IVegetable vegetable1, IVegetable vegetable2)
        {
            return vegetable1.Name.CompareTo(vegetable2.Name);
        }
    }
    public class CompareByCalories : IComparer<Calories>
    {
        public int Compare(Calories x, Calories y)
        {
            return x.CompareTo(y);
        }
    }
}