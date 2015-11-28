using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Manager Manager { get; set; } 
       
        public File(string name, DateTime date, Manager manager)
        {
            this.Name = name;
            this.Date = date;
            this.Manager = manager;
        }
        public File(int id, string name, DateTime date, Manager manager)
            : this(name, date, manager)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"{Id} - {Name}, {Date}, {Manager}";
        }
    }
}
