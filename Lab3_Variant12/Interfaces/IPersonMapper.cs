using Lab3_Variant12.Entities;

namespace Lab3_Variant12.Interfaces
{
    public interface IPersonMapper
    {
        string TypeName { get; }      // "Student" - так тип записується у файлі
        string[] Keys { get; }        // ключі атрибутів у файлі
        string[] Prompts { get; }     // підказки для консолі (в тому ж порядку)

        bool IsValid(int index, string value);   // перевірка регулярним виразом
        bool CanMap(Person person);              // чи це "наш" тип
        string[] GetValues(Person person);       // значення атрибутів (як рядки)
        Person Create(string[] values);          // створити об'єкт зі значень
    }
}
