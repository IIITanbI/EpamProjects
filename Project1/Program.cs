using System;
using System.Collections.Generic;
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
            var fryPotato = new FryPotato(200);

            Console.WriteLine(potato.Name + " " + potato.Calories + " " + potato.Weight);
            Console.WriteLine(cucmber.Name + " " + cucmber.Calories + " " + cucmber.Weight);
            Console.WriteLine(fryPotato.Name + " " + fryPotato.Calories + " " + fryPotato.Weight);
            
            Salad salad = new Salad();
            salad.Add(potato);
            salad.Add(fryPotato);
            salad.Add(cucmber);
            Console.WriteLine("ttoal calories = " + salad.TotalCalories);
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();
            var tt = salad.GetVegetables(0, 1000);
 
            Console.WriteLine();
            Console.WriteLine();
            salad.PrintVegetables();

        }
    }
}
