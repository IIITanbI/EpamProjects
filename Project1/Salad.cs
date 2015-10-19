using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Project1.Vegetables;

namespace Project1
{
    
    public interface ICloneCollection<T> : ICollection<T>, ICloneable
    {
        
    }
    
    public class Salad
    {
        private ICloneCollection<IVegetable> _ingridients = null;

        public Salad()
        {
            this._ingridients = new CloneList<IVegetable>();
        }
        public Salad(ICloneCollection<IVegetable> ingridients)
        {
            if (ingridients == null)
            {
                throw new ArgumentNullException();
            }
            var copy = (ICloneCollection<IVegetable>) ingridients.Clone();
            copy.Clear();
            copy.AddCloneRange(ingridients);

            this._ingridients = copy;
        }

        public void Sort<TKey>(Func<IVegetable, TKey> keySelector)
        {
            this.Sort(keySelector, null);
        }
        public void Sort<TKey>(Func<IVegetable, TKey> keySelector, IComparer<TKey> comparer)
        {
            IVegetable[] copy = this._ingridients.CloneObjectsToArray();
            var sorted = copy.OrderBy(keySelector, comparer);

            this._ingridients.Initialize(sorted);
            /*
                sort via array
                1. new array                    n memory
                2. InitizlizeClone              n copy
                3. linq sort without ToArray()  
                4. Initizlize with linq         

                sort via IMyCollection
                1. Clone collection             n copy + n memory
                2. InitizlizeClone              n copy
                3. linq sort with ToArray()     n memory
                4. Initizlize with linq
                
                
            */
        }
        public void Sort(IComparer<IVegetable> comparer)
        {
            IVegetable[] copy = this._ingridients.CloneObjectsToArray();
            Array.Sort(copy, comparer);
            this._ingridients.Initialize(copy);
        }
        public void Sort(Comparison<IVegetable> comparison)
        {
            IVegetable[] copy = this._ingridients.CloneObjectsToArray();
            Array.Sort(copy, comparison);
            this._ingridients.Initialize(copy);
        }

        public void PrintVegetables()
        {
            foreach (var vegetable in this._ingridients)
            {
                Console.WriteLine(vegetable.Name + " " + vegetable.Calories + " " + vegetable.Weight);
            }
        }

        public ICloneCollection<IVegetable> GetVegetables(double bottom, double upper)
        {
            var copy = (ICloneCollection<IVegetable>)this._ingridients.Clone();
            copy.Clear();

            var temp = this._ingridients.Where(x => x.Calories > bottom && x.Calories < upper);
            copy.AddCloneRange(temp);

            return copy;
        }
        public ICloneCollection<IVegetable> GetVegetables(Func<IVegetable, bool> func)
        {
            var copy = (ICloneCollection<IVegetable>)this._ingridients.Clone();
            copy.Clear();

            var temp = this._ingridients.Where(func);
            copy.AddCloneRange(temp);

            return copy;
        }
        public Calories TotalCalories
        {
            get
            {
                Calories res = new Calories();
                foreach (var vegetable in this._ingridients)
                {
                    res += vegetable.Calories * (vegetable.Weight / 100.0);
                }
                return res;
            }
        }
        public ICloneCollection<IVegetable> Ingridients => this._ingridients.CloneObjects();

        public void Add(IVegetable item)
        {
            _ingridients.Add(item);
        }
        public void Clear()
        {
            _ingridients.Clear();
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
