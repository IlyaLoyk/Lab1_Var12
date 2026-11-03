using System.Globalization;
using System.Text.RegularExpressions;

namespace Lab3_Variant12.Services
{
    /// <summary>Перевірка введених даних регулярними виразами.</summary>
    public static class Validator
    {
        private const string Letters = "A-Za-zА-Яа-яІіЇїЄєҐґ'’-";

        // Ім'я, прізвище: тільки літери (можна дефіс та апостроф)
        public static bool IsName(string value)
        {
            return Regex.IsMatch(value, "^[" + Letters + "]+$");
        }

        // Країна, спеціалізація: літери, слова можуть бути розділені пробілом
        public static bool IsText(string value)
        {
            return Regex.IsMatch(value, "^[" + Letters + "]+([ ][" + Letters + "]+)*$");
        }

        // Курс: від 1 до 6
        public static bool IsCourse(string value)
        {
            return Regex.IsMatch(value, "^[1-6]$");
        }

        // Середній бал: від 0 до 5 (4.8, 4,8, 5, 5.0)
        public static bool IsGrade(string value)
        {
            return Regex.IsMatch(value, "^(5([.,]0{1,2})?|[0-4]([.,][0-9]{1,2})?)$");
        }

        // Студентський квиток: 2 великі літери + 6 цифр (KB123456)
        public static bool IsStudentId(string value)
        {
            return Regex.IsMatch(value, "^[A-Z]{2}[0-9]{6}$");
        }

        // Закордонний паспорт: 2 великі літери + 6 цифр, або 9 цифр
        public static bool IsPassport(string value)
        {
            return Regex.IsMatch(value, "^([A-Z]{2}[0-9]{6}|[0-9]{9})$");
        }

        // Номер ліцензії кравця: TL + 4 цифри
        public static bool IsLicense(string value)
        {
            return Regex.IsMatch(value, "^TL[0-9]{4}$");
        }

        // Сценічний ідентифікатор співака: SG + 4 цифри
        public static bool IsStageId(string value)
        {
            return Regex.IsMatch(value, "^SG[0-9]{4}$");
        }

        // Перетворення оцінки у число (кома теж підходить)
        public static double ParseGrade(string value)
        {
            return double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}
