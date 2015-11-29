using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SaleInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Product Product { get; set; }
        public FileInformation FileInformation { get; set; }
        public decimal Cost { get; set; }

        public SaleInfo(DateTime date, FileInformation fileInformation, Client client, Product product, decimal cost)
        {
            this.Date = date;
            this.FileInformation = fileInformation;
            this.Client = client;
            this.Product = product;
            this.Cost = cost;
        }

        public SaleInfo(int id, DateTime date, FileInformation fileInformation, Client client, Product product, decimal cost)
            : this(date, fileInformation, client, product, cost)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"{Id} - {Date.ToShortDateString()} File: {FileInformation} Client: {Client} Product: {Product} {Cost}";
        }
    }
}
