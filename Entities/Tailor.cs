using System;

namespace Lab3_Variant12.Entities
{
    public class Tailor : Person
    {
        public Tailor(string firstName, string lastName) : base(firstName, lastName) { }

        public override void Study()
        {
            Console.WriteLine($"Кравець {LastName} самонавчається кравецькій справі.");
        }
    }
}