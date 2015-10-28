namespace Project2
{
    public enum ItemType
    {
        Punctuation,
        Word,
        None
    }

    public interface ISentenceItem
    {
        ItemType type { get; }
    }
}