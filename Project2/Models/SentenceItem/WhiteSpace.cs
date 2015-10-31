using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class WhiteSpace : ISentenceItem
    {
        private Symbol _symbol;

        public WhiteSpace() 
            : this(1)
        {
            
        }
        public WhiteSpace(int count)
        {
            _symbol = new Symbol(new string(' ', count));
        }

        public string Value
        {
            get { return _symbol.ToString(); } 
        }
        public Symbol Symbol
        {
            get { return this._symbol; }
        }
        public int Length
        {
            get { return _symbol.Length; }
        }
        public override string ToString()
        {
            return _symbol.ToString();
        }
    }
}
