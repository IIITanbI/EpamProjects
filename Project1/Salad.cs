using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Project1.Vegetables;

namespace Project1
{
    public static class Util
    {
        public static int CompareByWeight(this Salad source, IVegetable vegetable1, IVegetable vegetable2)
        {
            return vegetable1.Weight.CompareTo(vegetable2.Weight);
        }
        public static int CompareByCalories(this Salad source, IVegetable vegetable1, IVegetable vegetable2)
        {
            return vegetable1.Calories.CompareTo(vegetable2.Calories);
        }
        public static int CompareByName(this Salad source, IVegetable vegetable1, IVegetable vegetable2)
        {
            return vegetable1.Name.CompareTo(vegetable2.Name);
        }
    }


    public class Salad : ICollection<IVegetable>
    {
 
        private List<IVegetable> _ingridients;// = new List<IVegetable>();

        public Salad()
        {
            this._ingridients = new List<IVegetable>();
        }
        public Salad(IEnumerable<IVegetable> ingridients)
        {
            //   _ingridients = ingridients;
            this._ingridients.AddRange(ingridients);
        }

        public Calories TotalCalories
        {
            get
            {
                Calories res = new Calories();
                foreach (var vegetable in Ingridients)
                {
                    res += vegetable.Calories*(vegetable.Weight/100.0);
                }
                return res;
            }

        }
        public List<IVegetable> GetVegetables(double bottom, double upper)
        {
            return this.Ingridients.Where(x => x.Calories > bottom && x.Calories < upper).ToList();
        }
        public List<IVegetable> GetVegetables(Func<IVegetable, bool> func)
        {
            return this.Ingridients.Where(x => func(x) == true).ToList();
        }
        public void Sort(Comparison<IVegetable> comparison)
        {
            this._ingridients.Sort(comparison);
        }
        
        public void Sort( IComparer<IVegetable> comparer)
        {

            this._ingridients.Sort(comparer);
        }
        public void PrintVegetables()
        {
            foreach (var vegetable in Ingridients)
            {
                Console.WriteLine(vegetable.Name + " " + vegetable.Calories + " " + vegetable.Weight);
            }
        }

        public IEnumerable<IVegetable> Ingridients
        {
            get { return this._ingridients; }
        }
        public IEnumerator<IVegetable> GetEnumerator()
        {
            return _ingridients.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IVegetable item)
        {
            _ingridients.Add(item);
        }
        public void Clear()
        {
            _ingridients.Clear();
            bool lt = ((IList) _ingridients).IsReadOnly;
        }
        public bool Contains(IVegetable item)
        {
            return _ingridients.Contains(item);
        }
        public void CopyTo(IVegetable[] array, int arrayIndex)
        {
            _ingridients.CopyTo(array, arrayIndex);
        }
        public bool Remove(IVegetable item)
        {
            return _ingridients.Remove(item);
        }
        public int Count
        {
            get { return _ingridients.Count; }
        }
        public bool IsReadOnly
        {
            get
            {
              return ((IList)_ingridients).IsReadOnly;
            } 
        }
         

    }
}
