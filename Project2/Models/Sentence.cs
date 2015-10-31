using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Sentence : ISentence
    {
        private List<ISentenceItem> _sentenceItems = new List<ISentenceItem>();
        public SentceItemFactory SentceItemFactory { get; set; }
        public Sentence(string sentence)
        {
            var sentenceItemFactory = new SentceItemFactory();

            var list = TextParser.Parse(sentence);
            foreach (var item in list)
            {
                this.Add(sentenceItemFactory.GetItem(item));
            }
        }

        public Sentence()
        {
        }

        public Sentence(IEnumerable<ISentenceItem> source)
        {
            this._sentenceItems.AddRange(source);
        }

        public int TotalCount
        {
            get { return this._sentenceItems.Count; }
        }

        public int WordCount
        {
            get
            {
                return this._sentenceItems.Count(item => item is Word);
            }
        }

        public Punctuation EndSentence
        {
            get
            {
                return this._sentenceItems[this._sentenceItems.Count - 1] as Punctuation;
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
        public void Remove(ISentenceItem item)
        {
            this._sentenceItems.Remove(item);
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

        public bool IsInterrogative()
        {
            string temp = EndSentence?.Value;
            return (temp != null && temp.Contains('?'));
        }
        public bool IsExclamatory()
        {
            string temp = EndSentence?.Value;
            return (temp != null && temp.Contains('!'));
        }
        public bool IsDeclarative()
        {
            string temp = EndSentence?.Value;
            return (temp != null && temp.Contains('.'));
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
