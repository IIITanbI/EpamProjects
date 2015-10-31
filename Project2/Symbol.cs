using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public struct SymbolString : ISymbol
    {

        private static char[] _vowels = { 'a', 'e', 'i', 'o', 'u' };
        private string _chars;

        public SymbolString(string chars)
        {
            this._chars = chars ?? String.Empty;
        }
        public SymbolString(char value)
        {
            this._chars = string.Format("{0}", value);
           
        }

        public static char[] Vowels
        {
            get { return _vowels; }
            set { _vowels = value; }
        }

        public string Value
        {
            get { return _chars; }
            set
            {
                this._chars = value ?? String.Empty;
            }
        }
        public int Length
        {
            get { return _chars.Length; }
        }

        public static bool IsConsonant(SymbolString symbol)
        {
            return symbol.Value != null && symbol.Length == 1 && !_vowels.Contains(symbol.Value[0]);
        }
        public static bool IsVowel(SymbolString symbol)
        {
            return symbol.Value != null && symbol.Length == 1 && _vowels.Contains(symbol.Value[0]);
        }

        public override string ToString()
        {
            return this._chars;
        }
    }

    public interface ISymbol
    {
        int Length { get; }
        string Value { get; }
    }
}
