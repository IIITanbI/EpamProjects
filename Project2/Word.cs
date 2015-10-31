using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Word: ISentenceItem, IComparable<Word>
    {
        public ItemType Type { get; } = ItemType.Word;

        private SymbolString[] _symbols;
        public Word(string word)
        {
            this._symbols = word?.Select(c => new SymbolString(c)).ToArray() ?? new SymbolString[0];
        }

        public string Value
        {
            get { return this.ToString(); } 
        }

        public int Length
        {
            get { return this._symbols.Length; }
        }

        public int CompareTo(Word other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(_symbols.Length);
            foreach (var symbol in _symbols)
            {
                builder.Append(symbol);
            }
            return builder.ToString();
        }


    }
}
