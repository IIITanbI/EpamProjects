﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public struct SymbolString : ISymbol
    {
        private string _chars;

        public string Chars
        {
            get { return _chars; }
            set
            {
                this._chars = value ?? String.Empty;
            }
        }
        public SymbolString(string chars)
        {
            this._chars = chars ?? String.Empty;
        }

        public SymbolString(char value)
        {
            this._chars = string.Format("{0}", value);
        }

        public override string ToString()
        {
            return this._chars;
        }

        public int Length
        {
            get { return _chars.Length; }
        }
    }

    public interface ISymbol
    {
        int Length { get; }
    }

    public struct Symbol
    {
        public char Char { get; set; }
        
        public Symbol(char value)
        {
            this.Char = value;
        }

        public override string ToString()
        {
            return this.Char.ToString();
        }
    }
}
