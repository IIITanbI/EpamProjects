using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BL.Model;

namespace WindowsService
{
    public partial class MyService : ServiceBase
    {
        private Thread _workerThread;
        public MyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Tracker dataManager = new Tracker();
            _workerThread = new Thread(dataManager.OnStart);
            _workerThread.SetApartmentState(ApartmentState.STA);
            _workerThread.Start();
        }

        protected override void OnStop()
        {
            _workerThread.Abort();
        }

        public void AddLog(string log)
        {
            try
            {
                if (!EventLog.SourceExists("MyService"))
                {
                    EventLog.CreateEventSource("MyService", "MyService");
                }
                eventLog1.Source = "MyService";
                eventLog1.WriteEntry(log);
            }
            catch
            {
                // ignored
            }
        }
    }
}
