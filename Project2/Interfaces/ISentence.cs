using System.Collections.Generic;

namespace Project2
{
    public interface ISentence : IEnumerable<ISentenceItem>
    {
        Punctuation EndSentence { get; }

        ISentenceItem this[int index] { get; set; }

        int TotalCount { get; }
        int WordCount { get; }
        
    }
}