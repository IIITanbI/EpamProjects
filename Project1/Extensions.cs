using System;
using System.Collections.Generic;
using System.Linq;
using Project1.Vegetables;

namespace Project1
{
    public static class Extensions
    {
        public static void AddRange<T>(this ICloneCollection<T> source, IEnumerable<T> collection)
        {
            foreach (var value in collection)
            {
                source.Add(value);
            }
        }

        public static void AddCloneRange<T>(this ICloneCollection<T> source, IEnumerable<T> collection) where T:ICloneable
        {
            foreach (var value in collection)
            {
                source.Add((T)value.Clone());
            }
        }

        public static IVegetable[] CloneObjectsToArray<T>(this ICloneCollection<T> source) where T : ICloneable
        {
            IVegetable[] copy = new IVegetable[source.Count];
            int ind = 0;

            foreach (var veg in source)
            {
                copy[ind] = (IVegetable)veg.Clone();
                ind++;
            }

            return copy;
        }

        public static ICloneCollection<IVegetable> CloneObjects<T>(this ICloneCollection<T> source) where T : ICloneable
        {
            var copy = (ICloneCollection<IVegetable>)source.Clone();
            var tt = copy.ToArray();
            copy.InitializeClone(tt);

            return copy;
        }

        public static void Initialize<T>(this ICloneCollection<T> source, IEnumerable<T> collection)
        {
            source.Clear();
            source.AddRange(collection);
        }

        public static void InitializeClone<T>(this ICloneCollection<T> source, IEnumerable<T> collection) where T:ICloneable
        {
            source.Clear();
            source.AddCloneRange(collection);
        }
    }
}