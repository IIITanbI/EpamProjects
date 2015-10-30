using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
    public class Sentence : ISentence
    {
        private List<ISentenceItem> sentenceItems = new List<ISentenceItem>();
        

        public Sentence(string sentence)
        {
           sentenceItems.AddRange(Parse(sentence));
        }
        private List<ISentenceItem> Parse(string sentence)
        {
            var result = new List<ISentenceItem>();

            string pattern = @"(\w+(?:\-\w*)*)|(\p{P}+)|\s(?=[^\s])";

            var words = Regex.Matches(sentence, pattern);
            foreach (var word in words)
            {
                result.Add(new SentceItemFactory().GetItem(word.ToString()));
            }

            foreach (ISentenceItem word in result)
            {
                //Console.WriteLine(word);
            }
          //  Console.WriteLine();
          //  Console.WriteLine();
           

            return result;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (ISentenceItem item in sentenceItems)
            {
               builder.Append(item);
            }

            return builder.ToString();
        }

    }
}
