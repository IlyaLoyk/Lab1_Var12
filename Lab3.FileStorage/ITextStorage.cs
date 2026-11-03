namespace Lab3.FileStorage
{
    public interface ITextStorage
    {
        string[] ReadLines();
        void WriteLines(string[] lines);
    }
}
