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
        private ICollection<Account> _accounts;
        private IDictionary<Account, ICollection<CallStatistic>> _callStatistics; 
        public BillingSystem(ICollection<Account> accounts, IDictionary<Account, ICollection<CallStatistic>> callStatistics)
        {
            _accounts = accounts;
            _callStatistics = callStatistics;
        }


        public void Add(Account account)
        {
            this._accounts.Add(account);
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

        public void AddCallStatistic(Account account, CallStatistic callStatistic )
        {
            this._callStatistics[account].Add(callStatistic);
        }
        public Account GetAccount(PhoneNumber phoneNumber)
        {
            return this._accounts.FirstOrDefault(account => account.Agreement.PhoneNumber == phoneNumber);
        }
        public Account GetAccount(Client client)
        {
            return this._accounts.FirstOrDefault(account => account.Client == client);
        }


        public IEnumerable<CallStatistic> GetClientCalls(Client client, Func<CallStatistic, bool> predicate)
        {
            Account account = GetAccount(client);
            if (account == null)
                throw new Exception("Account can not be found");
            return this._callStatistics[account].Where(predicate);
        }

        public void RegisterEventForAts(IStation station)
        {
            station.CallInfoAdded += StationOnCallInfoAdded;
        }
    }
}
