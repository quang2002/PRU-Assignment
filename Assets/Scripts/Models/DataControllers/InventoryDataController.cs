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

        private InventoryLocalData InventoryLocalData { get; }
        private SkillBlueprint     SkillBlueprint     { get; }
        private SignalBus          SignalBus          { get; }

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

                this.InventoryLocalData.SkillData.TryGetValue(id ?? string.Empty, out var data);
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

        public bool IsSkillEquipped(string id)
        {
            return this.InventoryLocalData.EquippedSkill.Contains(id);
        }

        public SkillData GetSKillData(string id)
        {
            return this.InventoryLocalData.SkillData.TryGetValue(id, out var data)
                ? data
                : new SkillData
                {
                    ID    = id,
                    Level = 0
                };
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

        public void UseSkill(string id)
        {
            if (this.IsSkillEquipped(id)) return;

            var equippedSkill = this.InventoryLocalData.EquippedSkill;

            for (uint i = 0; i < equippedSkill.Length; i++)
            {
                if (equippedSkill[i] != null) continue;

                equippedSkill[i] = id;

                this.SignalBus.Fire(new EquippedSkillSignal
                {
                    Slot    = i,
                    SkillID = id
                });
                break;
            }
        }

        public void RemoveSkill(string id)
        {
            var equippedSkill = this.InventoryLocalData.EquippedSkill;

            for (uint i = 0; i < equippedSkill.Length; i++)
            {
                if (equippedSkill[i] != id) continue;
                equippedSkill[i] = null;

                this.SignalBus.Fire(new EquippedSkillSignal
                {
                    Slot    = i,
                    SkillID = null
                });
                break;
            }
        }
    }
}