using System;
using Lab3.FileStorage;
using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Services
{
    /// <summary>
    /// "База даних": додати, отримати, видалити, знайти.
    /// Залежить від абстракції ITextStorage (IoC), а не від конкретного файлу.
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly ITextStorage storage;
        private readonly PersonFileConverter converter;

        public PersonRepository(ITextStorage storage, PersonFileConverter converter)
        {
            this.storage = storage;
            this.converter = converter;
        }

        public Person[] GetAll()
        {
            return converter.ToPersons(storage.ReadLines());
        }

        public void ReplaceAll(Person[] persons)
        {
            storage.WriteLines(converter.ToLines(persons));
        }

        public bool Add(Person person)
        {
            Person[] all = GetAll();
            if (FindIndexById(all, person.UniqueId) != -1)
            {
                return false;   // такий ідентифікатор вже є
            }

            Array.Resize(ref all, all.Length + 1);
            all[all.Length - 1] = person;
            ReplaceAll(all);
            return true;
        }

        public bool RemoveById(string id)
        {
            Person[] all = GetAll();
            int index = FindIndexById(all, id);
            if (index == -1)
            {
                return false;
            }

            Person[] result = new Person[all.Length - 1];
            int k = 0;
            for (int i = 0; i < all.Length; i++)
            {
                if (i != index)
                {
                    result[k] = all[i];
                    k++;
                }
            }
            ReplaceAll(result);
            return true;
        }

        public Person FindById(string id)
        {
            Person[] all = GetAll();
            int index = FindIndexById(all, id);
            return index == -1 ? null : all[index];
        }

        public Person[] FindByLastName(string lastName)
        {
            Person[] all = GetAll();

            int count = 0;
            foreach (Person person in all)
            {
                if (SameText(person.LastName, lastName))
                {
                    count++;
                }
            }

            Person[] result = new Person[count];
            int k = 0;
            foreach (Person person in all)
            {
                if (SameText(person.LastName, lastName))
                {
                    result[k] = person;
                    k++;
                }
            }
            return result;
        }

        private static int FindIndexById(Person[] persons, string id)
        {
            for (int i = 0; i < persons.Length; i++)
            {
                if (SameText(persons[i].UniqueId, id))
                {
                    return i;
                }
            }
            return -1;
        }

        private static bool SameText(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
