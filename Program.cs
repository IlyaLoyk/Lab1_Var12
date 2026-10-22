using System;
using System.Collections.Generic;
using Lab3_Variant12.Entities;
using Lab3_Variant12.Services;

namespace Lab3_Variant12
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();

            while (true)
            {
                Console.Clear();
                Console.Write(@"==================================================
   СИСТЕМА УПРАВЛЕНИЯ БАЗОЙ ДАННЫХ (Вариант 12)   
==================================================
1. Показать всех студентов из файла
2. Добавить нового студента
3. Заполнить файл начальными данными (> 5 человек)
4. Выполнить поиск по Варианту 12
5. Выход
==================================================
Выберите действие (1-5): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAllStudents(fileManager);
                        break;
                    case "2":
                        AddStudent(fileManager);
                        break;
                    case "3":
                        SeedInitialData(fileManager);
                        break;
                    case "4":
                        SearchForeignExcellentStudents(fileManager);
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowAllStudents(FileManager fileManager)
        {
            Student[] students = fileManager.LoadStudents();
            if (students.Length == 0)
            {
                Console.WriteLine("Файл пуст или не существует. Добавьте данные (пункт 2 или 3).");
            }
            else
            {
                Console.WriteLine("--- Список всех студентов из файла ---");
                foreach (var s in students)
                {
                    Console.WriteLine($"[ID: {s.StudentID}] {s.LastName} {s.FirstName} | Курс: {s.Course} | Средний балл: {s.AverageGrade} | Страна: {s.Country} | Паспорт: {s.ForeignPassport}");
                }
            }
            WaitForKey();
        }

        static void AddStudent(FileManager fileManager)
        {
            Console.WriteLine("--- Добавление нового студента ---");

            Console.Write("Введите Имя: ");
            string firstName = Console.ReadLine();

            Console.Write("Введите Фамилию: ");
            string lastName = Console.ReadLine();
            if (!Validator.IsValidName(lastName))
            {
                Console.WriteLine("Ошибка! Фамилия должна содержать только буквы.");
                WaitForKey();
                return;
            }

            Console.Write("Введите Курс (1-6): ");
            int.TryParse(Console.ReadLine(), out int course);

            Console.Write("Введите Студенческий билет (формат: 2 буквы и 6 цифр, напр. KB123456): ");
            string studentId = Console.ReadLine();
            if (!Validator.IsValidStudentID(studentId))
            {
                Console.WriteLine("Ошибка! Формат студенческого билета должен быть типа KB123456.");
                WaitForKey();
                return;
            }

            Console.Write("Введите Средний балл (напр. 4.8): ");
            double.TryParse(Console.ReadLine().Replace(',', '.'), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double avgGrade);

            Console.Write("Введите Страну (напр. Poland): ");
            string country = Console.ReadLine();
            if (!Validator.IsValidName(country))
            {
                Console.WriteLine("Ошибка! Название страны должно содержать только буквы.");
                WaitForKey();
                return;
            }

            Console.Write("Введите Номер загранпаспорта: ");
            string passport = Console.ReadLine();
            if (!Validator.IsValidPassport(passport))
            {
                Console.WriteLine("Ошибка! Паспорт должен состоять из 9 цифр или 2 букв и 6 цифр.");
                WaitForKey();
                return;
            }

            Student[] currentList = fileManager.LoadStudents();
            List<Student> list = new List<Student>(currentList);
            list.Add(new Student(firstName, lastName, course, studentId, avgGrade, country, passport));

            fileManager.SaveStudents(list.ToArray());
            Console.WriteLine("\nСтудент успешно записан в файл!");
            WaitForKey();
        }

        static void SeedInitialData(FileManager fileManager)
        {
            Student[] initialStudents = new Student[]
            {
                new Student("John", "Smith", 1, "KB111111", 4.8, "USA", "FP100001"),
                new Student("Anna", "Schmidt", 1, "KB222222", 5.0, "Germany", "FP100002"),
                new Student("Ivan", "Petrov", 1, "KB333333", 4.9, "Ukraine", "FP100003"),
                new Student("Ali", "Hassan", 2, "KB444444", 4.7, "Egypt", "FP100004"),
                new Student("Maria", "Garcia", 1, "KB555555", 3.5, "Spain", "FP100005"),
                new Student("Pierre", "Dubois", 1, "KB666666", 4.6, "France", "FP100006")
            };

            fileManager.SaveStudents(initialStudents);
            Console.WriteLine("Файл перезаписан 6 тестовыми студентами!");
            WaitForKey();
        }

        static void SearchForeignExcellentStudents(FileManager fileManager)
        {
            Student[] students = fileManager.LoadStudents();
            int count = 0;

            Console.WriteLine("--- Результаты поиска (Иностранные студенты-отличники 1-го курса) ---");
            Console.WriteLine("----------------------------------------------------------------------------------");

            foreach (var student in students)
            {
                bool isFirstCourse = student.Course == 1;
                bool isExcellent = student.AverageGrade >= 4.5;
                bool isForeigner = student.Country.ToLower() != "ukraine" && student.Country.ToLower() != "украина";

                if (isFirstCourse && isExcellent && isForeigner)
                {
                    count++;
                    Console.WriteLine($"Фамилия: {student.LastName, -10} | Курс: {student.Course} | Средний балл: {student.AverageGrade} | Страна: {student.Country, -8} | Паспорт: {student.ForeignPassport}");
                }
            }

            Console.WriteLine("----------------------------------------------------------------------------------");
            Console.WriteLine($"Всего найдено: {count}");
            WaitForKey();
        }

        static void WaitForKey()
        {
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
}