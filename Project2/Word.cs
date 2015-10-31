using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Word: ISentenceItem, IComparable<Word>
    {
        private Symbol[] _symbols;
        public Word(string word)
        {
            this._symbols = word?.Select(c => new Symbol(c)).ToArray() ?? new Symbol[0];
        }

        public string Value
        {
            get { return this.ToString(); } 
        }

        public int Length
        {
            get { return this._symbols.Length; }
        }

        public bool StartWithVowel()
        {
            return _symbols.Length > 0 && Symbol.IsVowel(_symbols[0]);
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
