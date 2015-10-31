using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

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
}