namespace Lab3.FileStorage
{
    /// <summary>
    /// Абстракція сховища: вміє тільки читати і писати рядки.
    /// Нічого не знає ні про студентів, ні про консоль.
    /// </summary>
    public interface ITextStorage
    {
        string[] ReadLines();
        void WriteLines(string[] lines);
    }
}
