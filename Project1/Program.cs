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
    class Program
    {
        static void Main(string[] args)
        {
            Calories cl = new Calories(2, 3, 4);
            Calories c2 = 2*cl;

            var potato = new  Potato(100);
            var cucmber = new Cucumber(100);
            var carrot = new Carrot(200);

            var cust = new CustomVegetable("1", 2, new Calories());
            Console.WriteLine(cust);
            cust.Weight = 3;
            cust.Calories = new Calories();
            cust.Name = "13";
            Console.WriteLine(cust);

            Console.WriteLine(potato.Name + " " + potato.Calories + " " + potato.Weight);
            Console.WriteLine(cucmber.Name + " " + cucmber.Calories + " " + cucmber.Weight);
            Console.WriteLine(carrot.Name + " " + carrot.Calories + " " + carrot.Weight);
            List<IVegetable> lst = new List<IVegetable>();
            lst.Add(cucmber);


            Salad salad = new Salad(new CloneList<IVegetable>());
            salad.Add(potato);
            salad.Add(carrot);
            salad.Add(cucmber);
            Console.WriteLine("total calories = " + salad.TotalCalories);
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();
            var tt = salad.GetVegetables(0, 1000);
 
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();
            
            salad.Sort(VegetableComparasions.CompareByName);
            Console.WriteLine();
            Console.WriteLine();
           
            Console.WriteLine("BEFORE SRT");
            salad.PrintVegetables();
            salad.Sort(vegetable => vegetable.Calories, new CompareByCalories());
            
            Console.WriteLine("AFTER SRT");
            salad.PrintVegetables();
            salad.Sort(VegetableComparasions.CompareByWeight);
            Console.WriteLine("BEFORE REMOVE");
            salad.PrintVegetables();
            salad.Remove(potato);

            Console.WriteLine("AFTER REMOVE");
            salad.PrintVegetables();

            Console.WriteLine("TESTTTT");
            CloneList<IVegetable> list = new CloneList<IVegetable>();

            List<int> li = new List<int>();
           
            list.Add(potato);
            list.Add(cucmber);
            list.Add(carrot);
            foreach (var v in list)
            {
                Console.WriteLine(v.Name + " " + v.Calories + " " + v.Weight);
            }
            Console.WriteLine("CLONE");

            ICloneCollection<IVegetable> list2 = list.CloneObjects();
            foreach (var v in list2)
            {
                Console.WriteLine(v.Name + " " + v.Calories + " " + v.Weight);
            }
            list[0].Weight = 0;
            list[1].Weight = 0;
            list[2].Weight = 0;

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
