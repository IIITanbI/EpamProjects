using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    public class Chef
    {
        public string Name { get; } = "qwerty";
        public Salad Salad { get; }

        public Chef()
        {
            
        }
        public Chef(string name)
        {
            Name = name;
        }
        public Chef(string name, Salad salad)
            : this(name)
        {
            Salad = salad;
        }

        
    }
}
