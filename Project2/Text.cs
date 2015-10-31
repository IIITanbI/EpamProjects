using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project2
{
    public class TextParser
    {
        public static List<string> ParseFile(string filePath)
        {
            var result = new List<string>();

            string pattern = @"(?<=[\.!\?])\s*(?=[^\.!\?»])";
            string line;
            string remainingLine = null;
            var file = new StreamReader(filePath);

            while ((line = file.ReadLine()) != null)
            {
                string[] sentences = Regex.Split(line, pattern);
  
                if (!string.IsNullOrWhiteSpace(remainingLine))
                    sentences[0] = remainingLine + " " + sentences[0];

                remainingLine = !IsSentence(sentences[sentences.Length - 1]) ? sentences[sentences.Length - 1] : null;

                foreach (string sentence in sentences)
                {
                    if (string.IsNullOrWhiteSpace(sentence))
                        continue;
                    if (IsSentence(sentence))
                        result.Add(sentence);
                }
            }
            if (!string.IsNullOrWhiteSpace(remainingLine))
                result.Add(remainingLine);

            file.Close();
            return result;
        }
        public static List<string> ParseText(string text)
        {
            var result = new List<string>();

            string pattern = @"(?<=[\.!\?])\s*(?=[^\.!\?»])";
            string[] sentences = Regex.Split(text, pattern);

            foreach (string sentence in sentences)
            {
                if (string.IsNullOrWhiteSpace(sentence))
                    continue;
                result.Add(sentence);
            }

            foreach (var sentence in result)
            {
                Console.Write(sentence + " ");
            }
            Console.WriteLine();
            Console.WriteLine();

            return result;
        }
        public static List<string> Parse(string sentence)
        {
            var result = new List<string>();
            //string pattern = @"(\w+(?:\-\w*)*)|(\p{P}+)|\s(?=[^\s])";
            string pattern = @"[\.!\?]+$|(\w+(?:\-\w*)*)|\p{P}|\s(?=[^\s])";

            var items = Regex.Matches(sentence, pattern);
            foreach (var item in items)
                result.Add(item.ToString());
            

            return result;
        }

        public static bool IsSentence(string str)
        {
            return Regex.Match(str, @"[\.!\?]+$").Success;
        }
    }

    public class Text : IEnumerable<ISentence>
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
        
       
        public IEnumerable<ISentence> Sort()
        {
            return this.Sentences.OrderBy(s => s.WordCount).ToArray();
        }
        public void GetWordsByLength(int length)
        {
            foreach (var sentence in this._sentences)
            {
                //sentence.EndSentence.
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

        public IEnumerable<ISentence> Sentences
        {
            get { return this._sentences; }
        }

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
        public void Insert(int index, ISentence item)
        {
            this._sentences.Insert(index, item);
        }
        public void Remove(ISentence item)
        {
            this._sentences.Remove(item);
        }
        public void RemoveAt(int index)
        {
            this._sentences.RemoveAt(index);
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


        public IEnumerator<ISentence> GetEnumerator()
        {
            return this._sentences.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
