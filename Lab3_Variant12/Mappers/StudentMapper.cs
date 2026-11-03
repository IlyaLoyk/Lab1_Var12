using System.Globalization;
using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;
using Lab3_Variant12.Services;

namespace Lab3_Variant12.Mappers
{
    public class StudentMapper : IPersonMapper
    {
        public string TypeName => "Student";

        public string[] Keys => new string[]
        {
            "firstname", "lastname", "course", "studentId",
            "averageGrade", "country", "foreignPassport"
        };

        public string[] Prompts => new string[]
        {
            "Ім'я",
            "Прізвище",
            "Курс (1-6)",
            "Студентський квиток (2 великі літери + 6 цифр, напр. KB123456)",
            "Середній бал (0-5, напр. 4.8)",
            "Країна (напр. Poland)",
            "Закордонний паспорт (2 великі літери + 6 цифр, або 9 цифр)"
        };

        public bool IsValid(int index, string value)
        {
            switch (index)
            {
                case 0: return Validator.IsName(value);
                case 1: return Validator.IsName(value);
                case 2: return Validator.IsCourse(value);
                case 3: return Validator.IsStudentId(value);
                case 4: return Validator.IsGrade(value);
                case 5: return Validator.IsText(value);
                case 6: return Validator.IsPassport(value);
                default: return false;
            }
        }

        public bool CanMap(Person person)
        {
            return person is Student;
        }

        public string[] GetValues(Person person)
        {
            Student s = (Student)person;
            return new string[]
            {
                s.FirstName, s.LastName,
                s.Course.ToString(CultureInfo.InvariantCulture),
                s.StudentID,
                s.AverageGrade.ToString(CultureInfo.InvariantCulture),
                s.Country, s.ForeignPassport
            };
        }

        public Person Create(string[] values)
        {
            return new Student(values[0], values[1], int.Parse(values[2]), values[3],
                               Validator.ParseGrade(values[4]), values[5], values[6]);
        }
    }
}
