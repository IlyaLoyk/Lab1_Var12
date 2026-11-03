using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;
using Lab3_Variant12.Services;

namespace Lab3_Variant12.Mappers
{
    public class TailorMapper : IPersonMapper
    {
        public string TypeName => "Tailor";

        public string[] Keys => new string[]
        {
            "firstname", "lastname", "licenseNumber", "specialization"
        };

        public string[] Prompts => new string[]
        {
            "Ім'я",
            "Прізвище",
            "Номер ліцензії (TL + 4 цифри, напр. TL1001)",
            "Спеціалізація (необов'язково, Enter - пропустити)"
        };

        public bool IsValid(int index, string value)
        {
            switch (index)
            {
                case 0: return Validator.IsName(value);
                case 1: return Validator.IsName(value);
                case 2: return Validator.IsLicense(value);
                case 3: return value == "" || Validator.IsText(value);   // необов'язкове поле
                default: return false;
            }
        }

        public bool CanMap(Person person)
        {
            return person is Tailor;
        }

        public string[] GetValues(Person person)
        {
            Tailor t = (Tailor)person;
            return new string[] { t.FirstName, t.LastName, t.LicenseNumber, t.Specialization };
        }

        public Person Create(string[] values)
        {
            return new Tailor(values[0], values[1], values[2], values[3]);
        }
    }
}
