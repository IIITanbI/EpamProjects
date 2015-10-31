using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public class Text : IList<ISentence>
    {
        private List<ISentence> _sentences = new List<ISentence>();

        public Text()
        {
        }
        public Text(IEnumerable<ISentence> source)
        {
        }
        public Text(string text)
        {
            var list = TextParser.ParseFile(text);
            foreach (var val in list)
            {
                _sentences.Add(new Sentence(val));
            }
        }

        public bool IsReadOnly { get; } = false;
        public int Count
        {
            get { return this._sentences.Count; }
        }
       
        public ISentence this[int index]
        {
            get { return this._sentences[index]; }
            set { this._sentences[index] = value; }
        }

        public void Add(ISentence item)
        {
            this._sentences.Add(item);
        }
        public void Clear()
        {
            this._sentences.Clear();
        }
        public bool Contains(ISentence item)
        {
            return this._sentences.Contains(item);
        }
        public void CopyTo(ISentence[] array, int arrayIndex)
        {
            this._sentences.CopyTo(array, arrayIndex);
        }
        public int IndexOf(ISentence item)
        {
            return this._sentences.IndexOf(item);
        }
        public void Insert(int index, ISentence item)
        {
            this._sentences.Insert(index, item);
        }
        public bool Remove(ISentence item)
        {
           return this._sentences.Remove(item);
        }
        public void RemoveAt(int index)
        {
            this._sentences.RemoveAt(index);
        }

      
        public IEnumerable<ISentence> Sentences
        {
            get { return this._sentences; }
        }
        public IEnumerator<ISentence> GetEnumerator()
        {
            return this._sentences.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static Text operator +(Text l, Text r)
        {
            var result = new Text();

            foreach (var sentence in l.Sentences)
                result.Add(sentence);

            foreach (var sentence in r.Sentences)
                result.Add(sentence);

            return result;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < _sentences.Count; index++)
            {
                ISentence sentence = _sentences[index];

                builder.Append(sentence);
                if (index < _sentences.Count - 1)
                   builder.Append(new WhiteSpace());
            }
            return builder.ToString();
        }
    }
}
