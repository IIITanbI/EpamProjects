using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project3.Interfaces;

namespace Project3.model.BS
{
    class BillingSystem
    {
        private ICollection<IAccount> _acccounts;
        private IDictionary<IAccount, ICollection<CallStatistic>> _callStatistics; 
        public BillingSystem(ICollection<IAccount> acccounts, IDictionary<IAccount, ICollection<CallStatistic>> callStatistics)
        {
            _acccounts = acccounts;
            _callStatistics = callStatistics;
        }


        public void Add(IAccount account)
        {
            this._acccounts.Add(account);
        }
        private void StationOnCallInfoAdded(object sender, CallInfo callInfo)
        {
            var fromAccount  = GetAccount(callInfo.Source);
            var toAccount = GetAccount(callInfo.Target);
            if (fromAccount == null) return;
            double outCost = fromAccount.AddIncomingMinutes(callInfo.Duration.TotalMinutes);

            if (toAccount == null) return;
            double inCost = toAccount.AddIncomingMinutes(callInfo.Duration.TotalMinutes);

            AddCallStatistic(fromAccount, new CallStatistic(callInfo, outCost));
            AddCallStatistic(toAccount, new CallStatistic(callInfo, inCost));
        }

        public void AddCallStatistic(IAccount account, CallStatistic callStatistic )
        {
            this._callStatistics[account].Add(callStatistic);
        }
        public IAccount GetAccount(PhoneNumber phoneNumber)
        {
            return this._acccounts.FirstOrDefault(account => account.Agreement.PhoneNumber == phoneNumber);
        }
        public IAccount GetAccount(Client client)
        {
            return this._acccounts.FirstOrDefault(IAccount => IAccount.Client == client);
        }


        public IEnumerable<CallStatistic> GetClientCalls(Client client, Func<CallStatistic, bool> predicate)
        {
            IAccount account = GetAccount(client);
            if (account == null)
                throw new Exception("IAccount can not be found");
            return this._callStatistics[account].Where(predicate);
        }

        public void RegisterEventForAts(IStation station)
        {
            station.CallInfoAdded += StationOnCallInfoAdded;
        }
    }
}
