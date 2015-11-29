using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Project4
{
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
        }

    }
}
