using System;
using System.Collections.Generic;
using System.Linq;
using Project1.Vegetables;

namespace Project1
{
    public static class Extensions
    {
        public static void AddRange<T>(this IMyCollection<T> source, IEnumerable<T> collection)
        {
            foreach (var value in collection)
            {
                source.Add(value);
            }
        }

        public static void AddCloneRange<T>(this IMyCollection<T> source, IEnumerable<T> collection) where T:ICloneable
        {
            foreach (var value in collection)
            {
                source.Add((T)value.Clone());
            }
        }

        public static IIngredient[] CloneObjectsToArray<T>(this IMyCollection<T> source) where T : ICloneable
        {
            IIngredient[] copy = new IIngredient[source.Count];
            int ind = 0;

            lock (source)
            {
                foreach (var veg in source)
                {
                    copy[ind] = (IIngredient)veg.Clone();
                    ind++;
                }
            }

            return copy;
        }

        public static IMyCollection<IIngredient> CloneObjects<T>(this IMyCollection<T> source) where T : ICloneable
        {
            var copy = (IMyCollection<IIngredient>)source.Clone();
            var tt = copy.ToArray();
            copy.InitializeClone(tt);

            return copy;
        }

        public static void Initialize<T>(this IMyCollection<T> source, IEnumerable<T> collection)
        {
            source.Clear();
            source.AddRange(collection);
        }

        public static void InitializeClone<T>(this IMyCollection<T> source, IEnumerable<T> collection) where T:ICloneable
        {
            source.Clear();
            source.AddCloneRange(collection);
        }
    }
}