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
    public class Utility
    {
        public static object CloneObject(object objSource)
        {
            //step : 1 Get the type of source object and create a new instance of that type
            Type typeSource = objSource.GetType();
            object objTarget = Activator.CreateInstance(typeSource);

            //Step2 : Get all the properties of source object type
            PropertyInfo[] propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Step : 3 Assign all source property to taget object 's properties
            foreach (PropertyInfo property in propertyInfo)
            {
                //Check whether property can be written to
                if (property.CanWrite)
                {
                    //Step : 4 check whether property type is value type, enum or string type
                    if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                    {
                        property.SetValue(objTarget, property.GetValue(objSource, null), null);
                    }
                    //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                    else
                    {
                        object objPropertyValue = property.GetValue(objSource, null);
                        if (objPropertyValue == null)
                        {
                            property.SetValue(objTarget, null, null);
                        }
                        else
                        {
                            property.SetValue(objTarget, CloneObject(objPropertyValue), null);
                        }
                    }
                }
            }
            return objTarget;
        }
    }

   
    
    public class Salad 
    {
        private IMyCollection<IVegetable> _ingridients = null;

        public Salad()
        {
            this._ingridients = new MyList<IVegetable>();
        }
        public Salad(IMyCollection<IVegetable> ingridients)
        {
            var copy = (IMyCollection<IVegetable>) ingridients.Clone();
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

            var cp = (IMyCollection<IVegetable>)this._ingridients.Clone();
            cp.Initialize(copy);
            this._ingridients = cp;

            this._ingridients.Initialize(copy);
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
                Console.WriteLine(vegetable);
            }
        }

        public IMyCollection<IVegetable> GetVegetables(double bottom, double upper)
        {
            var copy = (IMyCollection<IVegetable>)this._ingridients.Clone();
            copy.Clear();

            var temp = this._ingridients.Where(x => x.CaloriesPer100G > bottom && x.CaloriesPer100G < upper);
            copy.AddCloneRange(temp);

            return copy;
        }
        public IMyCollection<IVegetable> GetVegetables(Func<IVegetable, bool> func)
        {
            var copy = (IMyCollection<IVegetable>)this._ingridients.Clone();
            copy.Clear();

            var temp = this._ingridients.Where(x => func(x) == true);
            copy.AddCloneRange(temp);

            return copy;
            //return this.Ingridients.Where(x => func(x) == true).ToList();
        }
        public Calories TotalCalories
        {
            get
            {
                Calories res = new Calories();
                foreach (var vegetable in this._ingridients)
                {
                    res += vegetable.CaloriesPer100G * (vegetable.Weight / 100.0);
                }
                return res;
            }
        }
        public IMyCollection<IVegetable> Ingridients
        {
            get
            {
                var cp = (IMyCollection<IVegetable>)this._ingridients.Clone();
                cp.Clear();
                cp.AddCloneRange(this._ingridients);
                return cp;
            }
        }

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
