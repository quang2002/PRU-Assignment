namespace Models.LocalData
{
    using System;

    [Serializable]
    public class SkillData
    {
        public string ID    { get; init; }
        public long   Level { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SkillData skillData && this.ID == skillData.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}