namespace Models.LocalData
{
    using System.Collections.Generic;
    using GDK.LocalData;

    public class InventoryLocalData : ILocalData
    {
        public const int MaxSkillSlot = 5;

        public Dictionary<string, SkillData> SkillData     { get; set; }
        public string[]                      EquippedSkill { get; set; }

        public void Initialize()
        {
            this.SkillData     = new Dictionary<string, SkillData>();
            this.EquippedSkill = new string[MaxSkillSlot];
        }
    }
}