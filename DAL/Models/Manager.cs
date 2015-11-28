using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Manager
    {
        public int Id { get; set; }
        public string SecondName { get; set; }

        public Manager(string secondName)
        {
            this.SecondName = secondName;
        }
        public Manager(int id, string secondName)
            : this(secondName)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"{Id} - {SecondName}";
        }
    }
}
