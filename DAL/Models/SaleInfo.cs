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
        public File File { get; set; }
        public decimal Cost { get; set; }

        public SaleInfo(DateTime date, File file, Client client, Product product, decimal cost)
        {
            this.Date = date;
            this.File = file;
            this.Client = client;
            this.Product = product;
            this.Cost = cost;
        }

        public SaleInfo(int id, DateTime date, File file, Client client, Product product, decimal cost)
            : this(date, file, client, product, cost)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"{Id} - {Date.ToShortDateString()} File: {File} Client: {Client} Product: {Product} {Cost}";
        }
    }
}
