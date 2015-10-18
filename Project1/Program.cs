using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Vegetables;

namespace Project1
{

    class MyList<T> : IMyCollection<T>
    {
        public List<T> list = new List<T>();

        public MyList()
        {
            
        } 

        public IEnumerator<T> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return list.Remove(item);
        }

        public int Count => list.Count;

        public bool IsReadOnly { get; }
        public object Clone()
        {
            var copy = new MyList<T>();

            foreach (var item in list)
            {
                copy.Add(item);
            }

            return copy;


        }
}

    class Program
    {
        static void Main(string[] args)
        {
            Calories cl = new Calories(2, 3, 4);
            Calories c2 = 2*cl;

            var potato = new  Potato(100);
            var cucmber = new Cucumber(100);
            var fryPotato = new FryPotato(200);

            Console.WriteLine(potato.Name + " " + potato.Calories + " " + potato.Weight);
            Console.WriteLine(cucmber.Name + " " + cucmber.Calories + " " + cucmber.Weight);
            Console.WriteLine(fryPotato.Name + " " + fryPotato.Calories + " " + fryPotato.Weight);
            
            Salad salad = new Salad();
            salad.Add(potato);
            salad.Add(fryPotato);
            salad.Add(cucmber);
            Console.WriteLine("total calories = " + salad.TotalCalories);
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();
            var tt = salad.GetVegetables(0, 1000);
 
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();
            
            salad.Sort(VegetableComparators.CompareByName);
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();

            salad.Sort(vegetable => vegetable.Calories, new CompareByCalories());
            salad.PrintVegetables();
            salad.Sort(VegetableComparators.CompareByWeight);


            Console.WriteLine("TESTTTT");
            MyList<IVegetable> list = new MyList<IVegetable>();
            list.Add(potato);
            list.Add(cucmber);
            list.Add(fryPotato);
            foreach (var v in list)
            {
                Console.WriteLine(v.Name + " " + v.Calories + " " + v.Weight);
            }
            Console.WriteLine("CLONE");

            IMyCollection<IVegetable> list2 = list.CloneObjects();
            foreach (var v in list2)
            {
                Console.WriteLine(v.Name + " " + v.Calories + " " + v.Weight);
            }
            list.list[0].Weight = 0;
            list.list[1].Weight = 0;
            list.list[2].Weight = 0;

            Console.WriteLine("TESTTTT");

            foreach (var v in list)
            {
                Console.WriteLine(v.Name + " " + v.Calories + " " + v.Weight);
            }
            Console.WriteLine("CLONE");

          
            foreach (var v in list2)
            {
                Console.WriteLine(v.Name + " " + v.Calories + " " + v.Weight);
            }
        }

    }
}
