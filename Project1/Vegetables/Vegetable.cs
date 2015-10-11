using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    public class Calories
    {
        public double Fat { get; set; }
        public double Protein { get; set; }
        public double Carbohydrate { get; set; }
        
        public Calories(double protein, double fat, double carbohydrate)
        {
            this.Protein = protein;
            this.Fat = fat;
            this.Carbohydrate = carbohydrate;
        }

        public double Count
        {
            get { return Fat*9 + Protein*4 + Carbohydrate*4; }
        }

    }
    public abstract class Vegetable
    {

        public double Weight { get; }

        protected Vegetable(double weight)
        {
            Weight = weight;
        }
    }
}
