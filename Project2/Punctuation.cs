using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Punctuation : ISentenceItem
    {
        private Symbol _symbol;

        public Punctuation(string punctuation)
        {
            this._symbol = new Symbol(punctuation);
        }

        public string Value
        {
            get { return _symbol.ToString(); }
        }

        public int Length { get { return _symbol.Length; } }

        public Symbol Symbol
        {
            get { return this._symbol; }
        }
 
        public override string ToString()
        {
            return _symbol.ToString();
        }
    }
}
