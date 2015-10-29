using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Project2
{

    public class Text
    {
        private List<ISentence> sentences = new List<ISentence>();

        public Text(string text)
        {
            sentences.AddRange(ReadParse("text.txt"));
        }

        public List<ISentence> ReadParse(string filePath)
        {
            var result = new List<ISentence>();

            string pattern = @"(?<=[\.!\?])\s*(?=[^\.!\?»])";
            string line;
            string remainingLine = null;
            var file = new System.IO.StreamReader(filePath);

            while ((line = file.ReadLine()) != null)
            {
                string[] sentences = Regex.Split(line, pattern);

                if (!string.IsNullOrWhiteSpace(remainingLine))
                    sentences[0] = remainingLine + " " + sentences[0];
                
                if (!Check(sentences[sentences.Length - 1]))
                    remainingLine = sentences[sentences.Length - 1];
                
                foreach (string sentence in sentences)
                {
                    if (string.IsNullOrWhiteSpace(sentence))
                        continue;
                    if (Check(sentence))
                        result.Add(new Sentence(sentence));
                }

            }

            foreach (var sentence in result)
            {
               // Console.WriteLine(sentence);
            }
            //Console.WriteLine();
            //Console.WriteLine();
            file.Close();

            return result;
        }

        public bool Check(string str)
        {
            return Regex.Match(str, @"[\.!\?]+$").Success;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int index = 0; index < sentences.Count; index++)
            {
                ISentence sentence = sentences[index];
                if (index > 0)
                    builder.Append(" " + sentence);
                else
                    builder.Append(sentence);
            }
            return builder.ToString();
        }
    }
}
