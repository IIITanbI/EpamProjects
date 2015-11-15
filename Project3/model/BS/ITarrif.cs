namespace Project3.model.BS
{
    public interface ITarrif
    {
        string Name { get; }
        double FreeMinutes { get; }
        double CostPerMinutes { get; }
        bool CanHaveNegativeBalanse { get; }
    }
}