using System;
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
        public List<string> ParseFile(string filePath)
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
        public List<string> ParseText(string text)
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

        public static bool IsSentence(string str)
        {
            return Regex.Match(str, @"[\.!\?]+$").Success;
        }
    }

    public class Text
    {
        private List<ISentence> _sentences = new List<ISentence>();

        
        public Text(string text)
        {
            TextParser tt = new TextParser();
            tt.ParseFile("text.txt");
            //sentences.AddRange(ParseFile("text.txt"));

            var list = tt.ParseFile("text.txt");
            foreach (var val in list)
            {
                _sentences.Add(new Sentence(val));
            }
        }

        public void GetWordsByLength(int length)
        {
            foreach (var s in this._sentences)
            {
                var set = new SortedSet<Word>();

                foreach (var item in s)
                {
                    if (item.Type == ItemType.Word && item.Length == length)
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
        public IEnumerable<ISentence> Sort()
        {
            return this.Sentences.OrderBy(s => s.WordCount).ToArray();
        } 
        public IEnumerable<ISentence> Sentences
        {
            get { return this._sentences; }
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
