using System.Text.RegularExpressions;

namespace Project2
{
    public class SentceItemFactory
    {
        public ISentenceItem GetItem(string item)
        {
            //WORD
            string pattern = @"^\w+(?:\-\w*)*$";
            if (Regex.Match(item, pattern).Success)
            {
                return new Word(item);
            }

            //PUNCTUATION
            pattern = @"^\p{P}+$";
            if (Regex.Match(item, pattern).Success)
            {
                return new Punctuation(item);
            }

            //WHITE SPACE
            pattern = @"^\s+$";
            if (Regex.Match(item, pattern).Success)
            {
                return new WhiteSpace();
            }

            return null;
        }
    }
}