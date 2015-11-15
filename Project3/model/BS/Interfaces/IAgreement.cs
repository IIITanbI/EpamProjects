using System;

namespace Project3.model.BS
{
    public interface IAgreement
    {
        Client Client { get; }
        PhoneNumber PhoneNumber { get; }
        DateTime AcceptedDate { get; }
        Account Account { get; }
    }
}
