using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Vegetables
{
    public class Calories : IComparable<Calories>, ICloneable
    {
        private double _protein = 0;
        private double _fat = 0;
        private double _carbohydrate = 0;

        public double Protein
        {
            get { return _protein; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Protein can not be less than 0");
                }
                _protein = value;
            }
        }
        public double Fat
        {
            get { return _fat; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Fat can not be less than 0");
                }
                _fat = value;
            }
        }
        public double Carbohydrate
        {
            get { return _carbohydrate; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Carbohydrate can not be less than 0");
                }
                _carbohydrate = value;
            }
        }

        public Calories()
        {
        }
        public Calories(Calories calories)
        {
            this.Protein = calories.Protein;
            this.Fat = calories.Fat;
            this.Carbohydrate = calories.Carbohydrate;
        }
        public Calories(double protein, double fat, double carbohydrate)
        {
            this.Protein = protein;
            this.Fat = fat;
            this.Carbohydrate = carbohydrate;
        }

        public double Count => Fat * 9 + Protein * 4 + Carbohydrate * 4;

        public static Calories operator +(Calories calories1, Calories calories2)
        {
            return new Calories(
                calories1.Protein + calories2.Protein,
                calories1.Fat + calories2.Fat,
                calories1.Carbohydrate + calories2.Carbohydrate);
        }
        public static Calories operator -(Calories calories1, Calories calories2)
        {
            return new Calories(
                calories1.Protein - calories2.Protein,
                calories1.Fat - calories2.Fat,
                calories1.Carbohydrate - calories2.Carbohydrate);
        }
        public static Calories operator *(Calories calories, double a)
        {
            return new Calories(
                calories.Protein*a,
                calories.Fat*a,
                calories.Carbohydrate*a);
        }
        public static Calories operator *(double a, Calories calories)
        {
            return calories*a;
        }

        public static implicit operator double (Calories calories)
        {
            return calories.Count;
        }
        public static implicit operator int (Calories calories)
        {
            return (int)calories.Count;
        }

        public override string ToString()
        {
            return Count + " calories";
        }

        public object Clone()
        {
            return new Calories(this);
        }

        public int CompareTo(Calories calories)
        {
            return Count.CompareTo(calories.Count);
        }
    }
}
