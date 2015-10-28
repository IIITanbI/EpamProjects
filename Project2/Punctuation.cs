using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Punctuation : ISentenceItem
    {
        public ItemType type { get; } = ItemType.Punctuation;
        private string punctuation;
        public Punctuation(string punctuation)
        {
            this.punctuation = punctuation;
        }

        public override string ToString()
        {
            return punctuation;
        }
    }
}
