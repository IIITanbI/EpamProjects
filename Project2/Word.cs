using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Word: ISentenceItem
    {
        public ItemType type { get; } = ItemType.Word;
        private string word;
        public Word(string word)
        {
            this.word = word;
        }

        public override string ToString()
        {
            return word;
        }
    }
}
