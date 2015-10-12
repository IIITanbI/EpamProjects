using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Vegetables;

namespace Project1
{
    public class Salad : ICollection<IVegetable> 
    {
        public static int CompareByWeight(Vegetable vegetable1, Vegetable vegetable2)
        {
            return vegetable1.Weight.CompareTo(vegetable2.Weight);
        }
        public static int CompareByCalories(Vegetable vegetable1, Vegetable vegetable2)
        {
            return vegetable1.Calories.CompareTo(vegetable2.Calories);
        }
        public static int CompareByName(Vegetable vegetable1, Vegetable vegetable2)
        {
            return vegetable1.Name.CompareTo(vegetable2.Name);
        }

        private List<IVegetable> _ingridients = new List<IVegetable>();

        public Salad()
        {
           
        }
        public Salad(List<IVegetable> ingridients)
        {
            _ingridients = ingridients;
        }

        public Calories TotalCalories
        {
            get
            {
                Calories res = new Calories();
                foreach (var vegetable in Ingridients)
                {
                    res += vegetable.Calories;
                }
                return res;
            }

        }
        public List<IVegetable> GetVegetables(double bottom, double upper)
        {
            return Ingridients.Where(x => x.Calories > bottom && x.Calories < upper).ToList();
        }
        public List<IVegetable> GetVegetables(Func<IVegetable, bool> func)
        {
            return Ingridients.Where(x => func(x) == true).ToList();
        }
        public void Sort(Comparison<IVegetable> comparison)
        {
            _ingridients.Sort(comparison);
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
            get { return _ingridients; }
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
