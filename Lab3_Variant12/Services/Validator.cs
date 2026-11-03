using System.Globalization;
using System.Text.RegularExpressions;

namespace Lab3_Variant12.Services
{
    public static class Validator
    {
        private const string Letters = "A-Za-zА-Яа-яІіЇїЄєҐґ'’-";

        public static bool IsName(string value)
        {
            return Regex.IsMatch(value, "^[" + Letters + "]+$");
        }

        public static bool IsText(string value)
        {
            return Regex.IsMatch(value, "^[" + Letters + "]+([ ][" + Letters + "]+)*$");
        }

        public static bool IsCourse(string value)
        {
            return Regex.IsMatch(value, "^[1-6]$");
        }

        public static bool IsGrade(string value)
        {
            return Regex.IsMatch(value, "^(5([.,]0{1,2})?|[0-4]([.,][0-9]{1,2})?)$");
        }

        public static bool IsStudentId(string value)
        {
            return Regex.IsMatch(value, "^[A-Z]{2}[0-9]{6}$");
        }

        public static bool IsPassport(string value)
        {
            return Regex.IsMatch(value, "^([A-Z]{2}[0-9]{6}|[0-9]{9})$");
        }

        public static bool IsLicense(string value)
        {
            return Regex.IsMatch(value, "^TL[0-9]{4}$");
        }

        public static bool IsStageId(string value)
        {
            return Regex.IsMatch(value, "^SG[0-9]{4}$");
        }

        public static double ParseGrade(string value)
        {
            return double.Parse(value.Replace(',', '.'), CultureInfo.InvariantCulture);
        }
    }
}
