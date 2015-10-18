namespace Project1.Vegetables
{
    class Carrot : Vegetable
    {
        public Carrot(double weight)
            : base("Carrot", weight, new Calories(1.3, 0.1, 6.9))
        {
        }
        public Carrot(Carrot carrot)
            : base(carrot)
        {
        }

        public override object Clone()
        {
            return new Carrot(this);
        }
    }
}