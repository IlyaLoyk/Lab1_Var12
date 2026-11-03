using System;
using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Services
{
    /// <summary>Знає всі доступні типи сутностей (через їх Mapper-и) і вміє знайти потрібний.</summary>
    public class MapperRegistry
    {
        public IPersonMapper[] Mappers { get; }

        public MapperRegistry(IPersonMapper[] mappers)
        {
            Mappers = mappers;
        }

        // За назвою типу з файлу ("Student"). Якщо типу немає - null.
        public IPersonMapper FindByType(string typeName)
        {
            foreach (IPersonMapper mapper in Mappers)
            {
                if (mapper.TypeName == typeName)
                {
                    return mapper;
                }
            }
            return null;
        }

        // За об'єктом: який Mapper вміє з ним працювати.
        public IPersonMapper FindFor(Person person)
        {
            foreach (IPersonMapper mapper in Mappers)
            {
                if (mapper.CanMap(person))
                {
                    return mapper;
                }
            }
            throw new InvalidOperationException("Для типу " + person.GetType().Name + " не зареєстровано Mapper.");
        }
    }
}
