namespace Models.DataControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using GDK.LocalData;
    using Models.Blueprint;
    using Models.LocalData;
    using ModestTree;
    using Signals;
    using Zenject;

    public class InventoryDataController : ILocalDataController
    {
        #region Inject

        public InventoryLocalData InventoryLocalData { get; }
        public SkillBlueprint     SkillBlueprint     { get; }
        public SignalBus          SignalBus          { get; }

        public InventoryDataController(InventoryLocalData inventoryLocalData,
                                       SkillBlueprint     skillBlueprint,
                                       SignalBus          signalBus)
        {
            this.InventoryLocalData = inventoryLocalData;
            this.SkillBlueprint     = skillBlueprint;
            this.SignalBus          = signalBus;
        }

        #endregion


        public IEnumerable<(uint? slot, SkillData data)> GetAllSkillData()
        {
            foreach (var (id, skillData) in this.InventoryLocalData.SkillData)
            {
                var index = this.InventoryLocalData.EquippedSkill.IndexOf(id);
                yield return (index > -1 ? (uint)index : null, skillData);
            }
        }

        public IEnumerable<(uint slot, SkillData data)> GetAllEquippedSkillData()
        {
            for (var i = 0; i < this.InventoryLocalData.EquippedSkill.Length; i++)
            {
                var id = this.InventoryLocalData.EquippedSkill[i];

                if (string.IsNullOrEmpty(id)) continue;

                this.InventoryLocalData.SkillData.TryGetValue(id, out var data);
                yield return ((uint)i, data);
            }
        }

        public void AddSkillLevel(string id, uint amount)
        {
            this.InventoryLocalData.SkillData.TryAdd(id, new SkillData()
            {
                ID    = id,
                Level = 0
            });
            this.InventoryLocalData.SkillData[id].Level += amount;
        }

        public void UseSkillAtSlot(string id, uint slot)
        {
            if (slot >= this.InventoryLocalData.EquippedSkill.Length) return;

            for (var i = 0; i < this.InventoryLocalData.EquippedSkill.Length; i++)
            {
                if (this.InventoryLocalData.EquippedSkill[i] != id) continue;
                this.InventoryLocalData.EquippedSkill[i] = null;
            }

            this.InventoryLocalData.EquippedSkill[(int)slot] = id;

            this.SignalBus.Fire(new EquippedSkillSignal
            {
                Slot    = slot,
                SkillID = id
            });
        }

        public SkillData GetSkillAtSlot(uint slot)
        {
            try
            {
                var id = this.InventoryLocalData.EquippedSkill[slot];
                return this.InventoryLocalData.SkillData[id];
            }
            catch
            {
                return null;
            }
        }

        public void RemoveSkill(string skillId)
        {
            for (var i = 0; i < this.InventoryLocalData.EquippedSkill.Length; i++)
            {
                if (!this.InventoryLocalData.EquippedSkill[i].Equals(skillId)) continue;
                this.InventoryLocalData.EquippedSkill[i] = null;
                break;
            }

            this.SignalBus.Fire<EquippedSkillSignal>();
        }
    }
}