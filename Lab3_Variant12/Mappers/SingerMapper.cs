using Lab3_Variant12.Entities;
using Lab3_Variant12.Interfaces;
using Lab3_Variant12.Services;

namespace Lab3_Variant12.Mappers
{
    public class SingerMapper : IPersonMapper
    {
        public string TypeName => "Singer";

        public string[] Keys => new string[]
        {
            "firstname", "lastname", "stageId", "voiceType"
        };

        public string[] Prompts => new string[]
        {
            "Ім'я",
            "Прізвище",
            "Сценічний ідентифікатор (SG + 4 цифри, напр. SG2001)",
            "Тип голосу (необов'язково, Enter - пропустити)"
        };

        public bool IsValid(int index, string value)
        {
            switch (index)
            {
                case 0: return Validator.IsName(value);
                case 1: return Validator.IsName(value);
                case 2: return Validator.IsStageId(value);
                case 3: return value == "" || Validator.IsText(value);   // необов'язкове поле
                default: return false;
            }
        }

        public bool CanMap(Person person)
        {
            return person is Singer;
        }

        public string[] GetValues(Person person)
        {
            Singer s = (Singer)person;
            return new string[] { s.FirstName, s.LastName, s.StageId, s.VoiceType };
        }

        public Person Create(string[] values)
        {
            return new Singer(values[0], values[1], values[2], values[3]);
        }
    }
}
