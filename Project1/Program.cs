using System;
using System.Collections;
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
            Calories c2 = new Calories();


            var potato = new Vegetable("potato", 100, new Calories(1,2,3));
            var cucmber = new Vegetable("Cucumber", 200, new Calories(4, 5, 6));
            var fryPotato = new Vegetable("fryPotato", 300, new Calories(7, 8, 9));
            
            Salad salad = new Salad();
            salad.Add(potato);
            salad.Add(fryPotato);
            salad.Add(cucmber);
            Console.WriteLine("total calories = " + salad.TotalCalories);
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();
            var tt = salad.GetVegetables(0, 1000);
 
            Console.WriteLine("BEFORE SOrt");
            Console.WriteLine();
            salad.PrintVegetables();
            salad.Sort(VegetableComparasions.CompareByName);

            Console.WriteLine("AFЕУК ЫЩКЕ");
            Console.WriteLine();
            salad.PrintVegetables();

            salad.Sort(vegetable => vegetable.CaloriesPer100G, new CompareByCalories());
            salad.PrintVegetables();
            salad.Sort(VegetableComparasions.CompareByWeight);


            Console.WriteLine("TESTTTT");
            MyList<IIngredient> list = new MyList<IIngredient>();
            list.Add(potato);
            list.Add(cucmber);
            list.Add(fryPotato);
            foreach (var v in list)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine("CLONE");

            IMyCollection<IIngredient> list2 = list.CloneObjects();
            foreach (var v in list2)
            {
                Console.WriteLine(v);
            }
            list[0].Weight = 0;
            list[1].Weight = 0;
            list[2].Weight = 0;

            Console.WriteLine("TESTTTT");

            foreach (var v in list)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine("CLONE");

          
            foreach (var v in list2)
            {
                Console.WriteLine(v);
            }
        }

    }
}
