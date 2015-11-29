using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FileInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public Manager Manager { get; set; } 
       
        public FileInformation(string name, DateTime date, Manager manager)
        {
            this.Name = name;
            this.Date = date;
            this.Manager = manager;
        }
        public FileInformation(int id, string name, DateTime date, Manager manager)
            : this(name, date, manager)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"File {Id} - {Name}, {Date}, Manager: {Manager}";
        }
    }
}
