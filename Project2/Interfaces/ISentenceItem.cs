using System;

namespace Project2
{
    public enum ItemType
    {
        Punctuation,
        Word,
        WhiteSpace,
        None
    }

    public interface ISentenceItem 
    {
        ItemType Type { get; }

        string Value { get; }

        int Length { get; }

    }
}