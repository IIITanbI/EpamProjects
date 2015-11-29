using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    class Program
    {
        static void Main(string[] args)
        {
            Service service = new Service();
            Console.CancelKeyPress += (x, y) =>
            {
                Console.WriteLine("Service is stopped");
                service.Stop();
            };
            Console.WriteLine("Service is running");
            service.Start();
            return;

            Task task1 = new Task(() => Go(1));
            Task task2 = new Task(() => Go(2));
            task1.Start();
            task2.Start();
            //Task.WaitAll(task1, task2);
            Console.Read();
        }

        static void Go(int i)
        {
            for(int j = 0; j < 100; j++)
            Console.WriteLine("current " + i + " thread = " + Task.CurrentId);
        }
    }
}
