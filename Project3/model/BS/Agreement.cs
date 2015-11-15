using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project3.model.BS
{
    class Agreement : IAgreement
    {
        public Client Client { get; }
        public PhoneNumber PhoneNumber { get; }
        public DateTime AcceptedDate { get; }
        public IAccount Account { get; set; }
        public Agreement(Client client, PhoneNumber phoneNumber, DateTime acceptedDate)
        {
            Client = client;
            PhoneNumber = phoneNumber;
            AcceptedDate = acceptedDate;
        }
    }
}
