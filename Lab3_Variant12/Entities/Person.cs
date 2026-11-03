namespace Lab3_Variant12.Entities
{
    /// <summary>Базовий клас. FirstName і LastName обов'язкові для всіх.</summary>
    public abstract class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        /// <summary>Скільки разів людина займалась навчанням (стан змінюється методом Study).</summary>
        public int StudyCount { get; protected set; }

        protected Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        /// <summary>Унікальний ідентифікатор (для пошуку та видалення).</summary>
        public abstract string UniqueId { get; }

        /// <summary>Який вид навчання: у ВУЗі чи самонавчання.</summary>
        public abstract string StudyKind { get; }

        public abstract void Study();
    }
}
