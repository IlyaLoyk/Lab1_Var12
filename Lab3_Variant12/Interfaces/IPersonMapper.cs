using Lab3_Variant12.Entities;

namespace Lab3_Variant12.Interfaces
{
    public interface IPersonMapper
    {
        string TypeName { get; }  
        string[] Keys { get; }      
        string[] Prompts { get; } 

        bool IsValid(int index, string value);
        bool CanMap(Person person);
        string[] GetValues(Person person);
        Person Create(string[] values);
    }
}
