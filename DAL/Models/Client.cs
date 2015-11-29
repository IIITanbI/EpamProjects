using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }

        public Client(string firstName, string secondName)
        {
            this.FirstName = firstName;
            this.SecondName = secondName;
        }
        public Client(int id, string firstName, string secondName)
            : this(firstName, secondName)
        {
            this.Id = id;
        }

        public override string ToString()
        {
            return $"Client {Id} - {FirstName}, {SecondName}";
        }
    }
}
