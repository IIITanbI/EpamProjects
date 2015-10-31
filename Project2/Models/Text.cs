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

        public IEnumerable<ISentence> Sort()
        {
            return this.Sentences.OrderBy(s => s.WordCount).ToArray();
        }
        public void GetWordsByLength(int length)
        {
            foreach (var sentence in this._sentences)
            {
                var sentence1 = sentence as Sentence;
                if (sentence1 != null && !sentence1.IsInterrogative())
                    continue;

                var set = new SortedSet<Word>();

                foreach (var item in sentence)
                {
                    if (item is Word && item.Length == length)
                    {
                        set.Add((Word)item);
                    }
                }

                foreach (var word in set)
                {
                    Console.WriteLine(word);
                }
            }
        }
        public void DeleteAllVowelWords(int length)
        {
            foreach (var sentence in this._sentences)
            {
                for(int i = 0; i < sentence.TotalCount;)
                {
                    var item = sentence[i] as Word;
                    if (item != null && item.Length == length && item.StartWithVowel())
                    {
                        (sentence as Sentence)?.Remove(item);
                    }
                    else i++;
                }
            }
        }
        public void ReplaceBySequence(int sentenceIndex, int wordLength, string subString)
        {
            Sentence sentence = this[sentenceIndex] as Sentence;
            if (sentence == null) return;

            var sub = TextParser.Parse(subString);
            var sentenceItemFactory = new SentceItemFactory();
            

            for(int i = 0; i < sentence.TotalCount; )
            {
                Word word = sentence[i] as Word;
                if (word?.Length == wordLength)
                {
                    sentence.RemoveAt(i);
                    foreach (var item in sub)
                    {
                        sentence.Insert(i, sentenceItemFactory.GetItem(item));
                        i++;
                    }
                    //1 2 3 4 5 6 7 
                    //0 1 2 3 4 5 6 7 
                    //1 2 8 9 4 5 6 7
                }
                else i++;
            }

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
