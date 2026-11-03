using System;
using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;
using Lab3_Variant12.Services;

namespace Lab3_Variant12
{
    /// <summary>Увесь ввід-вивід на консоль знаходиться тільки тут.</summary>
    public class ConsoleMenu
    {
        private readonly IPersonRepository repository;
        private readonly MapperRegistry registry;
        private readonly PersonSearchService searchService;

        public ConsoleMenu(IPersonRepository repository, MapperRegistry registry, PersonSearchService searchService)
        {
            this.repository = repository;
            this.registry = registry;
            this.searchService = searchService;
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                PrintMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAll(); break;
                    case "2": AddPerson(); break;
                    case "3": RemovePerson(); break;
                    case "4": FindByLastName(); break;
                    case "5": FindById(); break;
                    case "6": RunVariantTask(); break;
                    case "7": DoActions(); break;
                    case "8": SeedDatabase(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("Невірний вибір."); break;
                }
            }
        }

        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("================ БАЗА ДАНИХ (Варіант 12) ================");
            Console.WriteLine("1. Показати всіх осіб з файлу");
            Console.WriteLine("2. Додати особу (Student / Tailor / Singer)");
            Console.WriteLine("3. Видалити особу за ідентифікатором");
            Console.WriteLine("4. Знайти за прізвищем");
            Console.WriteLine("5. Знайти за ідентифікатором");
            Console.WriteLine("6. Завдання варіанту: іноземні студенти-відмінники 1-го курсу");
            Console.WriteLine("7. Виконати дії особи (Study, Dive)");
            Console.WriteLine("8. Заповнити файл початковими даними");
            Console.WriteLine("0. Вихід");
            Console.Write("Ваш вибір: ");
        }

        private void ShowAll()
        {
            Person[] persons = repository.GetAll();
            if (persons.Length == 0)
            {
                Console.WriteLine("Файл порожній. Додайте дані (пункт 2 або 8).");
                return;
            }

            Console.WriteLine("Всього осіб: " + persons.Length);
            foreach (Person person in persons)
            {
                PrintPerson(person);
            }
        }

        private void AddPerson()
        {
            IPersonMapper[] mappers = registry.Mappers;

            Console.WriteLine("Оберіть тип:");
            for (int i = 0; i < mappers.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + mappers[i].TypeName);
            }
            Console.Write("Ваш вибір: ");

            int number;
            if (!int.TryParse(Console.ReadLine(), out number) || number < 1 || number > mappers.Length)
            {
                Console.WriteLine("Невірний тип.");
                return;
            }

            IPersonMapper mapper = mappers[number - 1];
            string[] values = new string[mapper.Keys.Length];
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = ReadValid(mapper, i);
            }

            Person person = mapper.Create(values);
            if (repository.Add(person))
            {
                Console.WriteLine("Особу додано у файл.");
            }
            else
            {
                Console.WriteLine("Особа з таким ідентифікатором вже існує.");
            }
        }

        // Питаємо, поки користувач не введе коректне значення
        private string ReadValid(IPersonMapper mapper, int index)
        {
            while (true)
            {
                Console.Write(mapper.Prompts[index] + ": ");
                string input = (Console.ReadLine() ?? "").Trim();
                if (mapper.IsValid(index, input))
                {
                    return input;
                }
                Console.WriteLine("Некоректне значення, спробуйте ще раз.");
            }
        }

        private void RemovePerson()
        {
            Console.Write("Введіть ідентифікатор для видалення: ");
            string id = (Console.ReadLine() ?? "").Trim();

            if (repository.RemoveById(id))
            {
                Console.WriteLine("Особу видалено.");
            }
            else
            {
                Console.WriteLine("Особу не знайдено.");
            }
        }

        private void FindByLastName()
        {
            Console.Write("Введіть прізвище: ");
            string lastName = (Console.ReadLine() ?? "").Trim();

            Person[] found = repository.FindByLastName(lastName);
            Console.WriteLine("Знайдено: " + found.Length);
            foreach (Person person in found)
            {
                PrintPerson(person);
            }
        }

        private void FindById()
        {
            Console.Write("Введіть ідентифікатор: ");
            string id = (Console.ReadLine() ?? "").Trim();

            Person person = repository.FindById(id);
            if (person == null)
            {
                Console.WriteLine("Особу не знайдено.");
            }
            else
            {
                PrintPerson(person);
            }
        }

        // Завдання варіанту 12: дані беремо з файлу, потім рахуємо
        private void RunVariantTask()
        {
            Person[] all = repository.GetAll();
            Student[] found = searchService.FindForeignExcellentFirstYear(all);

            Console.WriteLine("Іноземні студенти-відмінники 1-го курсу:");
            foreach (Student student in found)
            {
                PrintPerson(student);
            }
            Console.WriteLine("Кількість: " + found.Length);
        }

        // Методи сутностей змінюють стан, а повідомлення виводимо тут
        private void DoActions()
        {
            Console.Write("Введіть ідентифікатор особи: ");
            string id = (Console.ReadLine() ?? "").Trim();

            Person person = repository.FindById(id);
            if (person == null)
            {
                Console.WriteLine("Особу не знайдено.");
                return;
            }

            person.Study();
            Console.WriteLine(person.LastName + ": " + person.StudyKind
                + " (кількість занять: " + person.StudyCount + ")");

            IDiveable diver = person as IDiveable;
            if (diver != null)
            {
                diver.Dive();
                Console.WriteLine(person.LastName + " пірнає (кількість пірнань: " + diver.DiveCount + ")");
            }
            else
            {
                Console.WriteLine(person.LastName + " не вміє пірнати.");
            }
        }

        private void SeedDatabase()
        {
            Console.Write("Файл буде перезаписано. Продовжити? (y/n): ");
            string answer = (Console.ReadLine() ?? "").Trim().ToLower();
            if (answer != "y")
            {
                Console.WriteLine("Скасовано.");
                return;
            }

            Person[] data = SeedData.Create();
            repository.ReplaceAll(data);
            Console.WriteLine("Файл заповнено. Записано осіб: " + data.Length);
        }

        // Один рядок на особу: [Тип] ключ=значення; ...
        private void PrintPerson(Person person)
        {
            IPersonMapper mapper = registry.FindFor(person);
            string[] keys = mapper.Keys;
            string[] values = mapper.GetValues(person);

            string line = "[" + mapper.TypeName + "] ";
            for (int i = 0; i < keys.Length; i++)
            {
                line += keys[i] + "=" + values[i];
                if (i < keys.Length - 1)
                {
                    line += "; ";
                }
            }
            Console.WriteLine(line);
        }
    }
}
