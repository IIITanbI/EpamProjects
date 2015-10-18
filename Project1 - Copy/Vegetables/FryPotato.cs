namespace Project1.Vegetables
{
    class FryPotato : Potato
    {
        public FryPotato(double weight)
            : base("Fry Potato", weight, new Calories(2.8, 9.5, 23.4))
        {
        }

        public override object Clone()
        {
            return new FryPotato(this.Weight);
        }
    }
}