using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    class CustomVegetable : Vegetable
    {
        public string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }
        public double Weight
        {
            get { return base.Weight; }
            set { base.Weight = value; }
        }
        public Calories Calories
        {
            get { return base.Calories; }
            set { base.Calories = value; }
        }
        //public string Name { get; set; }
        //public double Weight { get; set; }
        //public Calories Calories { get; set; }

        public CustomVegetable(string name, double weight, Calories calories) 
            : base(name, weight, calories)
        {
        }
        public CustomVegetable(CustomVegetable customVegetable)
            : base(customVegetable)
        {

        }


        //public override string ToString()
        //{
        //    return "Name: " + this.Name + '\n'
        //           + "Weight: " + this.Weight + '\n'
        //           + "CaloriesPer100G: " + this.Calories + '\n';

        //}
        public override object Clone()
        {
            return new CustomVegetable(this);
        }
    }
}
