using System;
using System.Text.RegularExpressions;
using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Services
{
    /// <summary>
    /// Перетворює масив об'єктів у рядки файлу і навпаки.
    /// Формат у файлі:
    ///   Student JohnSmith
    ///   {
    ///     "firstname": "John",
    ///     "lastname": "Smith"
    ///   };
    /// </summary>
    public class PersonFileConverter
    {
        private static readonly Regex AttributeRegex = new Regex("\"(\\w+)\"\\s*:\\s*\"([^\"]*)\"");

        private readonly MapperRegistry registry;

        public PersonFileConverter(MapperRegistry registry)
        {
            this.registry = registry;
        }

        // Об'єкти -> рядки (поелементно: кожен атрибут окремим рядком)
        public string[] ToLines(Person[] persons)
        {
            string[] lines = new string[0];

            foreach (Person person in persons)
            {
                IPersonMapper mapper = registry.FindFor(person);
                string[] keys = mapper.Keys;
                string[] values = mapper.GetValues(person);

                Append(ref lines, mapper.TypeName + " " + person.FirstName + person.LastName);
                Append(ref lines, "{");
                for (int i = 0; i < keys.Length; i++)
                {
                    string comma = i < keys.Length - 1 ? "," : "";
                    Append(ref lines, "  \"" + keys[i] + "\": \"" + values[i] + "\"" + comma);
                }
                Append(ref lines, "};");
            }
            return lines;
        }

        // Рядки -> об'єкти (різних типів в одному масиві)
        public Person[] ToPersons(string[] lines)
        {
            Person[] result = new Person[0];
            int i = 0;

            while (i < lines.Length)
            {
                string header = lines[i].Trim();
                i++;
                if (header == "")
                {
                    continue;
                }

                // Перше слово заголовка - тип ("Student"), далі - назва об'єкта
                IPersonMapper mapper = registry.FindByType(header.Split(' ')[0]);
                string[] values = new string[mapper == null ? 0 : mapper.Keys.Length];

                // Читаємо атрибути до рядка "};"
                while (i < lines.Length && lines[i].Trim() != "};")
                {
                    Match match = AttributeRegex.Match(lines[i]);
                    if (match.Success && mapper != null)
                    {
                        int index = Array.IndexOf(mapper.Keys, match.Groups[1].Value);
                        if (index >= 0)
                        {
                            values[index] = match.Groups[2].Value;
                        }
                    }
                    i++;
                }
                i++; // пропускаємо "};"

                // Невідомий тип або некоректні дані - такий запис пропускаємо
                if (mapper != null && AllValid(mapper, values))
                {
                    Append(ref result, mapper.Create(values));
                }
            }
            return result;
        }

        private static bool AllValid(IPersonMapper mapper, string[] values)
        {
            for (int k = 0; k < values.Length; k++)
            {
                if (values[k] == null || !mapper.IsValid(k, values[k]))
                {
                    return false;
                }
            }
            return true;
        }

        private static void Append<T>(ref T[] array, T item)
        {
            Array.Resize(ref array, array.Length + 1);
            array[array.Length - 1] = item;
        }
    }
}
