using System.Collections.Generic;
using Project1.Vegetables;

namespace Project1
{
    public static class VegetableComparasions
    {
        public static int CompareByWeight(IVegetable ingredient1, IVegetable ingredient2)
        {
            return   ingredient1.Weight.CompareTo(ingredient2.Weight);
        }
        public static int CompareByCalories(IVegetable ingredient1, IVegetable ingredient2)
        {
            return ingredient1.CaloriesPer100G.CompareTo(ingredient2.CaloriesPer100G);
        }
        public static int CompareByName<T>(T ingredient1, T ingredient2) where T:IVegetable
        {
            return ingredient1.Name.CompareTo(ingredient2.Name);
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