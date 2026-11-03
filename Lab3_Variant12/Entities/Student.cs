using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Entities
{
    public class Student : Person, IDiveable
    {
        public int Course { get; private set; }
        public string StudentID { get; }
        public double AverageGrade { get; }
        public string Country { get; }
        public string ForeignPassport { get; }
        public int DiveCount { get; private set; }

        public Student(string firstName, string lastName, int course, string studentID,
                       double averageGrade, string country, string foreignPassport)
            : base(firstName, lastName)
        {
            Course = course;
            StudentID = studentID;
            AverageGrade = averageGrade;
            Country = country;
            ForeignPassport = foreignPassport;
        }

        public override string UniqueId => StudentID;
        public override string StudyKind => "навчання у ВУЗі";

        public override void Study()
        {
            StudyCount++;
        }

        public void Dive()
        {
            DiveCount++;
        }

        public void NextCourse()
        {
            if (Course < 6)
            {
                Course++;
            }
        }
    }
}
