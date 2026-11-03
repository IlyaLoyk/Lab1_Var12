using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Entities
{
    public class Tailor : Person, IDiveable
    {
        public string LicenseNumber { get; }

        public string Specialization { get; }

        public int DiveCount { get; private set; }

        public Tailor(string firstName, string lastName, string licenseNumber, string specialization)
            : base(firstName, lastName)
        {
            LicenseNumber = licenseNumber;
            Specialization = string.IsNullOrWhiteSpace(specialization) ? "General" : specialization;
        }

        public override string UniqueId => LicenseNumber;
        public override string StudyKind => "самонавчання";

        public override void Study()
        {
            StudyCount++;
        }

        public void Dive()
        {
            DiveCount++;
        }
    }
}
