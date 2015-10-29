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
            string pattern = @"\w+(?:\-\w*)*";
            if (Regex.Match(item, pattern).Success)
            {
                return new Word(item);
            }
            return new Punctuation(item);
        }
    }
    public class Sentence : ISentence
    {
        private List<ISentenceItem> sentenceItems = new List<ISentenceItem>();
        

        public Sentence(string sentence)
        {
           Console.WriteLine();
           sentenceItems.AddRange(Parse(sentence));
        }
        private List<ISentenceItem> Parse(string sentence)
        {
            var result = new List<ISentenceItem>();

            string pattern = @"(\w+(?:\-\w*)*)|(\p{P}+)|\s(?=[^\s])";

            var words = Regex.Matches(sentence, pattern);
            bool flag = true;
            foreach (var word in words)
            {
                //if (string.IsNullOrWhiteSpace(word.ToString()))
                //{
                //    if (flag)
                //        result.Add(new SentceItemFactory().GetItem(word.ToString()));

                //    flag = false;
                //    continue;
                //}
                //else flag = true;
                
                result.Add(new SentceItemFactory().GetItem(word.ToString()));
            }

            foreach (ISentenceItem word in result)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();
            Console.WriteLine();
           

            return result;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            
            for (int index = 0; index < sentenceItems.Count; index++)
            {
                ISentenceItem item = sentenceItems[index];
                ItemType cur = item.type;

                if (cur == ItemType.Word)
                    builder.Append(item);
                else builder.Append(item);
            }

            return builder.ToString();
        }
    }
}
