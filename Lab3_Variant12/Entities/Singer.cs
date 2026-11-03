using Lab3_Variant12.Interfaces;

namespace Lab3_Variant12.Entities
{
    public class Singer : Person, IDiveable
    {
        public string StageId { get; }

        public string VoiceType { get; }

        public int DiveCount { get; private set; }

        public Singer(string firstName, string lastName, string stageId, string voiceType)
            : base(firstName, lastName)
        {
            StageId = stageId;
            VoiceType = string.IsNullOrWhiteSpace(voiceType) ? "Unknown" : voiceType;
        }

        public override string UniqueId => StageId;
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
