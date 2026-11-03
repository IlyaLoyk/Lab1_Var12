using Lab3_Variant12.Entities;

namespace Lab3_Variant12.Services
{
    public static class SeedData
    {
        public static Person[] Create()
        {
            return new Person[]
            {
                new Student("John", "Smith", 1, "KB111111", 4.8, "USA", "FP100001"),
                new Student("Anna", "Schmidt", 1, "KB222222", 5.0, "Germany", "FP100002"),
                new Student("Ivan", "Petrov", 1, "KB333333", 4.9, "Ukraine", "FP100003"),
                new Student("Ali", "Hassan", 2, "KB444444", 4.7, "Egypt", "FP100004"),
                new Student("Maria", "Garcia", 1, "KB555555", 3.5, "Spain", "FP100005"),
                new Student("Pierre", "Dubois", 1, "KB666666", 4.6, "France", "FP100006"),
                new Tailor("Oleg", "Shvets", "TL1001", "Suits"),
                new Tailor("Olena", "Kravchenko", "TL1002", ""),
                new Singer("Iryna", "Melnyk", "SG2001", "Soprano"),
                new Singer("Taras", "Bondar", "SG2002", "")
            };
        }
    }
}
