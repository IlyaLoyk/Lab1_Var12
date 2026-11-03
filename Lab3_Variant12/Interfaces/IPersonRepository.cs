using Lab3_Variant12.Entities;

namespace Lab3_Variant12.Interfaces
{
    public interface IPersonRepository
    {
        Person[] GetAll();
        bool Add(Person person);
        bool RemoveById(string id);
        Person FindById(string id);
        Person[] FindByLastName(string lastName);
        void ReplaceAll(Person[] persons);
    }
}
