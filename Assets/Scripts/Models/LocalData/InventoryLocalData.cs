namespace Models.LocalData
{
    using System.Collections.Generic;
    using GDK.LocalData.Scripts;

    public class InventoryLocalData : ILocalData
    {
        public Dictionary<string, uint> SkillToLevel      { get; set; }
        public Dictionary<uint, string> EquippedSkill     { get; set; }
        public uint                     UnlockedSkillSlot { get; set; }

        public void Initialize()
        {
            this.SkillToLevel      = new Dictionary<string, uint>();
            this.EquippedSkill     = new Dictionary<uint, string>();
            this.UnlockedSkillSlot = 0;
        }
    }
}