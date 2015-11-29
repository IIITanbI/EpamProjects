using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using DAL.Repository;

namespace BL.Model
{
    public class Tracker
    {
        public void OnStart()
        {
            var filePath = ConfigurationManager.AppSettings["FolderPath"];
            var fileExtension = ConfigurationManager.AppSettings["FileExtension"];
            var watcher = new Watcher(filePath, fileExtension);

            watcher.CreatedFile += (sender, info) => { CreateTask(info); };
            Task.WaitAll();
            watcher.Run(() =>
            {
                var result = Console.Read() != 'q';
                return result;
            });
        }

        public void CreateTask(FileInfo fileInfo)
        {
            ;
        }
        static readonly object Locker = new object();
        public void AddInformationToTheDb(string fileName)
        {
            var records = new Reader(fileName).Read();

            var parser = new Parser();
            var fileInformation = parser.ParseFileName(fileName);
            //var saleInfoRepository = new SaleInfoRepository();
            using (var fileInformationRepository = new FileInformationRepository())
            {
                fileInformationRepository.Add(fileInformation);
                using (var saleInfoRepository = new SaleInfoRepository())
                {
                    foreach (var record in records)
                    {
                        var saleInfo = parser.ParseRecord(record);
                        lock (Locker)
                        {
                            saleInfoRepository.Add(saleInfo);
                        }
                    }
                }
            }
            //saleInfoRepository.Dispose();

        }
    }
}
