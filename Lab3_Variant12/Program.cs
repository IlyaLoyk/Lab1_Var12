using System;
using System.Text;
using Lab3.FileStorage;
using Lab3_Variant12.Interfaces;
using Lab3_Variant12.Mappers;
using Lab3_Variant12.Services;

namespace Lab3_Variant12
{
    internal static class Program
    {
        private static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            ITextStorage storage = new TextFileStorage("persons.txt");

            MapperRegistry registry = new MapperRegistry(new IPersonMapper[]
            {
                new StudentMapper(),
                new TailorMapper(),
                new SingerMapper()
            });

            PersonFileConverter converter = new PersonFileConverter(registry);
            IPersonRepository repository = new PersonRepository(storage, converter);

            ConsoleMenu menu = new ConsoleMenu(repository, registry, new PersonSearchService());
            menu.Run();
        }
    }
}
