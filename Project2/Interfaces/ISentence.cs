using System.Collections.Generic;

namespace Project2
{
    public interface ISentence : IEnumerable<ISentenceItem>
    {
        int TotalCount { get; }
        int WordCount { get; }
        
    }
}