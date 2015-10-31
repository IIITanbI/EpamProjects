using System;
using System.Collections;
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
        private List<ISentenceItem> _sentenceItems = new List<ISentenceItem>();
        
        public Sentence(string sentence)
        {
           _sentenceItems.AddRange(Parse(sentence));
        }

        private List<ISentenceItem> Parse(string sentence)
        {
            var result = new List<ISentenceItem>();
            var sentenceItemFactory = new SentceItemFactory();
            //string pattern = @"(\w+(?:\-\w*)*)|(\p{P}+)|\s(?=[^\s])";
            string pattern = @"[\.!\?]+$|(\w+(?:\-\w*)*)|\p{P}|\s(?=[^\s])";

            var words = Regex.Matches(sentence, pattern);
            foreach (var word in words)
            {
                result.Add(sentenceItemFactory.GetItem(word.ToString()));
            }

            return result;
        }

        public int TotalCount
        {
            get { return this._sentenceItems.Count; }
        }

        public int WordCount
        {
            get
            {
                return this._sentenceItems.Count(item => item.Type == ItemType.Word);
            }
        }

        public ISentenceItem this[int index]
        {
            get { return this._sentenceItems[index]; }
            set { this._sentenceItems[index] = value; }
        }

        public void Add(ISentenceItem item)
        {
            this._sentenceItems.Add(item);
        }
        public void Insert(int index, ISentenceItem item)
        {
            this._sentenceItems.Insert(index, item);
        }
        public void RemoveAt(int index)
        {
            this._sentenceItems.RemoveAt(index);
        }

        public IEnumerator<ISentenceItem> GetEnumerator()
        {
            return this._sentenceItems.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (ISentenceItem item in _sentenceItems)
            {
                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}
