using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using Lab3_Variant12.Entities;

namespace Lab3_Variant12.Services
{
    // Класс работы с файлами (запись и чтение по формату из методички)
    public class FileManager
    {
        private string filePath = "students.txt";

        // Сохранение массива студентов в файл по строго заданному формату
        public void SaveStudents(Student[] students)
        {
            using (StreamWriter writer = new StreamWriter(filePath, false))
            {
                foreach (var s in students)
                {
                    writer.WriteLine($"Student {s.LastName}");
                    writer.WriteLine("{");
                    writer.WriteLine($"  \"firstname\": \"{s.FirstName}\",");
                    writer.WriteLine($"  \"lastname\": \"{s.LastName}\",");
                    writer.WriteLine($"  \"course\": \"{s.Course}\",");
                    writer.WriteLine($"  \"studentId\": \"{s.StudentID}\",");
                    writer.WriteLine($"  \"averageGrade\": \"{s.AverageGrade.ToString(CultureInfo.InvariantCulture)}\",");
                    writer.WriteLine($"  \"country\": \"{s.Country}\",");
                    writer.WriteLine($"  \"foreignPassport\": \"{s.ForeignPassport}\"");
                    writer.WriteLine("};");
                }
            }
        }

        // Чтение и поэлементный парсинг данных из файла
        public Student[] LoadStudents()
        {
            if (!File.Exists(filePath)) return new Student[0];

            List<Student> list = new List<Student>();
            string[] lines = File.ReadAllLines(filePath);

            string firstName = "", lastName = "", studentId = "", country = "", passport = "";
            int course = 0;
            double avgGrade = 0;

            foreach (var line in lines)
            {
                string trimmed = line.Trim();

                if (trimmed.StartsWith("\"firstname\":")) 
                    firstName = GetValue(trimmed);
                else if (trimmed.StartsWith("\"lastname\":")) 
                    lastName = GetValue(trimmed);
                else if (trimmed.StartsWith("\"course\":")) 
                    int.TryParse(GetValue(trimmed), out course);
                else if (trimmed.StartsWith("\"studentId\":")) 
                    studentId = GetValue(trimmed);
                else if (trimmed.StartsWith("\"averageGrade\":")) 
                    double.TryParse(GetValue(trimmed), NumberStyles.Any, CultureInfo.InvariantCulture, out avgGrade);
                else if (trimmed.StartsWith("\"country\":")) 
                    country = GetValue(trimmed);
                else if (trimmed.StartsWith("\"foreignPassport\":")) 
                    passport = GetValue(trimmed);
                else if (trimmed == "};")
                {
                    list.Add(new Student(firstName, lastName, course, studentId, avgGrade, country, passport));
                }
            }

            return list.ToArray();
        }

        // Вспомогательный метод парсинга значений
        private string GetValue(string line)
        {
            int firstQuote = line.IndexOf(':', 0);
            int start = line.IndexOf('"', firstQuote);
            int end = line.LastIndexOf('"');
            if (start != -1 && end > start)
            {
                return line.Substring(start + 1, end - start - 1);
            }
            return "";
        }
    }
}