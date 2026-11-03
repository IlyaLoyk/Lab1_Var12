using System;
using Lab3_Variant12.Entities;

namespace Lab3_Variant12.Services
{
    public class PersonSearchService
    {
        private const double ExcellentGrade = 4.5;

        private static readonly string[] HomeCountries = { "ukraine", "україна", "украина" };

        public Student[] FindForeignExcellentFirstYear(Person[] persons)
        {
            int count = 0;
            foreach (Person person in persons)
            {
                if (person is Student student && IsMatch(student))
                {
                    count++;
                }
            }

            Student[] result = new Student[count];
            int k = 0;
            foreach (Person person in persons)
            {
                if (person is Student student && IsMatch(student))
                {
                    result[k] = student;
                    k++;
                }
            }
            return result;
        }

        private static bool IsMatch(Student student)
        {
            return student.Course == 1
                && student.AverageGrade >= ExcellentGrade
                && !IsHomeCountry(student.Country);
        }

        private static bool IsHomeCountry(string country)
        {
            foreach (string home in HomeCountries)
            {
                if (string.Equals(country, home, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
