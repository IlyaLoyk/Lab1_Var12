namespace Lab3_Variant12.Interfaces
{
    /// <summary>Вміння "Пірнати". Його може мати будь-яка сутність.</summary>
    public interface IDiveable
    {
        int DiveCount { get; }
        void Dive();
    }
}
