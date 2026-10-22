using System;
using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Entities
{
    public class Student : Person, IDiveable
    {
        public int Course { get; set; }
        public string StudentID { get; set; }
        public double AverageGrade { get; set; }
        public string Country { get; set; }
        public string ForeignPassport { get; set; }

        public Student(string firstName, string lastName, int course, string studentID, double averageGrade, string country, string foreignPassport)
            : base(firstName, lastName)
        {
            Course = course;
            StudentID = studentID;
            AverageGrade = averageGrade;
            Country = country;
            ForeignPassport = foreignPassport;
        }

        public override void Study()
        {
            Console.WriteLine($"Студент {LastName} навчається у ВУЗі.");
        }

        public void Dive()
        {
            Console.WriteLine($"Студент {LastName} вміє пірнати.");
        }
    }
}