using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Product(string name)
        {
            this.Name = name;
        }
        public Product(int id, string name)
            : this(name)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"Product {Id} - {Name}";
        }
    }
}
