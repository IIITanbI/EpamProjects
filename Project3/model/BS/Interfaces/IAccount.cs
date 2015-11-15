namespace Project3.model.BS
{
    public interface IAccount
    {
        Client Client { get; }
        IAgreement Agreement { get; }
        ITarrif Tarrif { get; }
        Statistic Statistic { get; }
        void Pay(double money);
        bool ChangeTarrif(ITarrif newTarrif);
        double AddIncomingMinutes(double minutes);
        double AddOutcomingMinutes(double minutes);
    }
}