using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Entities
{
    public class Singer : Person, IDiveable
    {
        /// <summary>Обов'язковий атрибут: сценічний ідентифікатор (унікальний).</summary>
        public string StageId { get; }

        /// <summary>Необов'язковий атрибут: значення за замовчуванням "Unknown".</summary>
        public string VoiceType { get; }

        public int DiveCount { get; private set; }

        public Singer(string firstName, string lastName, string stageId, string voiceType)
            : base(firstName, lastName)
        {
            StageId = stageId;
            VoiceType = string.IsNullOrWhiteSpace(voiceType) ? "Unknown" : voiceType;
        }

        public override string UniqueId => StageId;
        public override string StudyKind => "самонавчання (тренує вокал)";

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
