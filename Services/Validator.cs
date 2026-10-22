using System.Text.RegularExpressions;

namespace Lab3_Variant12.Services
{
    public static class Validator
    {
        public static bool IsValidName(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            return Regex.IsMatch(input, @"^[A-Za-zА-Яа-яІіЇїЄє\s-]+$");
        }

        // Проверка студенческого (2 буквы + 6 цифр, напр. KB123456)
        public static bool IsValidStudentID(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;
            return Regex.IsMatch(id, @"^[A-Za-z]{2}\d{6}$");
        }

        // Проверка паспорта (9 цифр или 2 буквы + 6 цифр)
        public static bool IsValidPassport(string passport)
        {
            if (string.IsNullOrWhiteSpace(passport)) return false;
            return Regex.IsMatch(passport, @"^(\d{9}|[A-Za-z]{2}\d{6})$");
        }
    }
}