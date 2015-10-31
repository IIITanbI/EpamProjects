using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public struct Symbol : ISymbol
    {
        public static readonly char[] DefaultVowels = { 'a', 'e', 'i', 'o', 'u' };
        private string _chars;

        public Symbol(string chars)
        {
            this._chars = chars ?? String.Empty;
        }
        public Symbol(char value)
        {
            this._chars = $"{value}";
        }

        public static char[] Vowels { get; set; } = DefaultVowels;

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

        public static bool IsConsonant(Symbol symbol)
        {
            return symbol.Value != null && symbol.Length == 1 && !Vowels.Contains(symbol.Value[0]);
        }
        public static bool IsVowel(Symbol symbol)
        {
            return symbol.Value != null && symbol.Length == 1 && Vowels.Contains(symbol.Value[0]);
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
