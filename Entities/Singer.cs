using System;
using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Entities
{
    public class Singer : Person, IDiveable
    {
        public Singer(string firstName, string lastName) : base(firstName, lastName) { }

        public override void Study()
        {
            Console.WriteLine($"Співак {LastName} тренує вокал.");
        }

        public void Dive()
        {
            Console.WriteLine($"Співак {LastName} також вміє пірнати.");
        }
    }
}