using System.Dynamic;
using System.Text.RegularExpressions;

namespace Project2
{
    public class SentceItemFactory : ISentceItemFactory
    {
        public ISentenceItem Create(string item)
        {
            //WORD
            if (TextParser.IsWord(item))
                return new Word(item);

            //PUNCTUATION
            if (TextParser.IsPunctuation(item))
                return new Punctuation(item);

            //WHITE SPACE
            if (TextParser.IsWhiteSpace(item))
                return new WhiteSpace();

            return null;
        }
    }
    
}