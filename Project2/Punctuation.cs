using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Punctuation : ISentenceItem
    {
        public ItemType Type { get; } = ItemType.Punctuation;
        private SymbolString _symbol;

        public Punctuation(string punctuation)
        {
            this._symbol = new SymbolString(punctuation);
        }

        public string Value
        {
            get { return _symbol.ToString(); }
        }

        public int Length { get { return 1; } }

        public SymbolString Symbol
        {
            get { return this._symbol; }
        }

        

        public override string ToString()
        {
            return _symbol.ToString();
        }
    }
}
