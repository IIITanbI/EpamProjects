using System;
using System.IO;
using DAL.Models;

namespace BL.Model
{
    public class Parser
    {
        public FileInformation ParseFileName(string filePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (fileName == null)
                return null;

            string[] str = fileName.Split('_');
            if (str.Length != 2) 
                throw new Exception("Wrong file name");

            string secondName = str[0];
            DateTime date = DateTime.Parse(str[1]);

            return new FileInformation(fileName, date, new Manager(secondName));
        }
        public SaleInfo ParseRecord(Record record)
        {
            string[] strings = record.Client.Split(' ');

            DateTime date = record.Date;
            DAL.Models.FileInformation fileInformation = null;
            Client client = null;
            Product product = new Product(record.Product);
            decimal cost = record.Cost;

            switch (strings.Length)
            {
                case 1:
                    client = new Client("", strings[0]);
                    break;
                case 2:
                    client = new Client(strings[0], strings[1]);
                    break;
            }


            return new SaleInfo(date, fileInformation, client, product, cost);
        }


    }
}
