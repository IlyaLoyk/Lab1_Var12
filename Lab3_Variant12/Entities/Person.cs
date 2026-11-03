namespace Lab3_Variant12.Entities
{
    public abstract class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public int StudyCount { get; protected set; }

        protected Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public abstract string UniqueId { get; }

        public abstract string StudyKind { get; }

        public abstract void Study();
    }
}
