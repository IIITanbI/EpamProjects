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
        public static readonly char[] DefaultVowels = { 'A', 'E', 'I', 'O', 'U' };
        public static readonly char[] DefaultConsonants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };
        public static char[] _vowels = { 'A', 'E', 'I', 'O', 'U' };
        public static char[] _consonants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };

        private string _chars;

        public Symbol(string chars)
        {
            this._chars = chars ?? String.Empty;
        }
        public Symbol(char value)
        {
            this._chars = $"{value}";
        }

        public static char[] Vowels
        {
            get { return _vowels; }
            set
            {
                _vowels = new char[value.Length];
                for (int i = 0; i < _vowels.Length; i++)
                    _vowels[i] = char.ToUpper(value[i]);
            }
        }
        public static char[] Consonants
        {
            get { return _consonants; }
            set
            {
                _consonants = new char[value.Length];
                for (int i = 0; i < _consonants.Length; i++)
                    _consonants[i] = char.ToUpper(value[i]);
            }
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

        public static bool IsConsonant(Symbol symbol)
        {
            return symbol.Value != null && symbol.Length == 1 && Consonants.Contains(char.ToUpper(symbol.Value[0]));
        }
        public static bool IsVowel(Symbol symbol)
        {
            return symbol.Value != null && symbol.Length == 1 && Vowels.Contains(char.ToUpper(symbol.Value[0]));
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
