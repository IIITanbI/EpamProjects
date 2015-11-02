using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Project2
{
    public class TextParser
    {
        public static SentceItemFactory SentceItemFactory { get; set; } = new SentceItemFactory();

        public static Text ParseFile(string filePath)
        {
            Text text = new Text();
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

            foreach (var sentence in result)
                text.Add(ParseSentence(sentence));
            
            return text;
        }
        public static Text ParseText(string text)
        {
            Text _text = new Text();
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
                _text.Add(ParseSentence(sentence));

            return _text;
        }
        public static ISentence ParseSentence(string sentence)
        {
            var result = new Sentence();
            //string pattern = @"(\w+(?:\-\w*)*)|(\p{P}+)|\s(?=[^\s])";
            string pattern = @"[\.!\?]+$|(\w+(?:\-\w*)*)|\p{P}|\s(?=[^\s])";

            var items = Regex.Matches(sentence, pattern);
            foreach (var item in items)
            {
                result.Add(SentceItemFactory.GetItem(item.ToString()));
            }

            return result;
        }


        public static bool IsWord(string str)
        {
            string pattern = @"^\w+(?:\-\w*)*$";
            return (Regex.Match(str, pattern).Success);
        }
        public static bool IsPunctuation(string str)
        {
            string pattern = @"^\p{P}+$";
            return (Regex.Match(str, pattern).Success);
        }
        public static bool IsWhiteSpace(string str)
        {
            string pattern = @"^\s+$";
            return (Regex.Match(str, pattern).Success);
        }
        public static bool IsSentence(string str)
        {
            return Regex.Match(str, @"[\.!\?]+$").Success;
        }
    }
}