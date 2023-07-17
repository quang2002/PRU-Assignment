namespace Signals
{
    using Models.Blueprint;

    public class EquippedSkillSignal
    {
        public uint   Slot    { get; init; }
        public string SkillID { get; init; }
    }
}