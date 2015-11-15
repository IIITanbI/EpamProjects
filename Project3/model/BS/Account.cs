using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project3.model.BS
{
    public class Account
    {
        public Client Client { get; }
        public IAgreement Agreement { get; }

        public Tarrif Tarrif { get; private set; }
        public TimeSpan TarrifChangePeriod { get; private set; }
        public TimeSpan PaymentPeriod { get; private set; }

        public Statistic Statistic { get; private set; }
       
        
        public Account(IAgreement agreement, Tarrif tarrif, TimeSpan tarrifChangePeriod, TimeSpan paymentPeriod)
        {
            if (agreement == null) throw new ArgumentNullException(nameof(agreement));
            if (tarrif == null) throw new ArgumentNullException(nameof(tarrif));

            this.Agreement = agreement;
            this.Client = Agreement.Client;

            this.Statistic = new Statistic()
            {
                LastDatePayment = Agreement.AcceptedDate,
                LastChangePlan =  Agreement.AcceptedDate
            };

            this.Tarrif = tarrif;
            this.TarrifChangePeriod = tarrifChangePeriod;
            this.PaymentPeriod = paymentPeriod;
        }

        public void Pay(double money)
        {
            this.Statistic.Balans += money;
            this.Statistic.LastDatePayment = DateTime.Now;
        }

        public bool ChangeTarrif(Tarrif newTarrif)
        {
            if (this.Tarrif == newTarrif)
                return true;

            var curDate = DateTime.Now;
            
            if (this.Statistic.LastDatePayment + this.PaymentPeriod >= curDate)
            {
                this.Tarrif = newTarrif;
                this.Statistic.LastChangePlan = curDate;
                return true;
            }
            return false;
        }

        public double AddIncomingMinutes(double minutes)
        {
            if (minutes < 0) 
                throw new Exception("Minutes can not be negative");
            this.Statistic.IncomingMinutes += minutes;
            return 0;    
        }
        public double AddOutcomingMinutes(double minutes)
        {
            if (minutes < 0)
                throw new Exception("Minutes can not be negative");

            double cost = 0;
            if (this.Statistic.LastDatePayment + this.TarrifChangePeriod > DateTime.Now)
            {
                cost = Cost(minutes);
            }
            else if (this.Statistic.Balans > 0)
            {
                cost = Cost(minutes);
            }
            else if (this.Tarrif.FreeMinutes - this.Statistic.OutcomingMinutes >= 0)
            {
                cost = Cost(minutes);
            }
            else if (Tarrif.CanHaveNegativeBalanse)
            {
                cost = Cost(minutes);
            }
            else
            {
                cost = 0;
                minutes = 0;
            }
            this.Statistic.OutcomingMinutes += minutes;
            this.Statistic.TotalCost += cost;
            return cost;
        }

        public double Cost(double minutes)
        {
            double remainingFreeMinutes = this.Tarrif.FreeMinutes - this.Statistic.OutcomingMinutes;
            if (remainingFreeMinutes > 0)
            {
                return Math.Max(minutes - remainingFreeMinutes, 0)*this.Tarrif.CostPerMinutes;
            }
            else
                return minutes * this.Tarrif.CostPerMinutes;
        }
    }
}
