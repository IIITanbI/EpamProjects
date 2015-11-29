using System.Threading;
using BL.Model;

namespace Project4
{
    public class Service
    {
        private readonly Thread _workerThread;

        public Service()
        {
            Tracker dataManager = new Tracker();
            _workerThread = new Thread(dataManager.OnStart);
            _workerThread.SetApartmentState(ApartmentState.STA);
        }

        public void Start()
        {
            _workerThread.Start();
        }

        public void Stop()
        {
            _workerThread.Abort();
        }
    }
}