namespace Models.DataControllers
{
    using GDK.LocalData.Scripts;
    using Models.LocalData;

    public class InventoryDataController : ILocalDataController
    {
        #region Inject

        public InventoryLocalData InventoryLocalData { get; }

        public InventoryDataController(InventoryLocalData inventoryLocalData)
        {
            this.InventoryLocalData = inventoryLocalData;
        }

        #endregion

        public void AddSkillLevel(string id, uint amount)
        {
            this.InventoryLocalData.SkillToLevel.TryAdd(id, 0);
            this.InventoryLocalData.SkillToLevel[id] += amount;
        }

        public void UseSkillAtSlot(string id, uint slot)
        {
            if (slot >= this.InventoryLocalData.UnlockedSkillSlot) return;

            foreach (var (keySlot, valueSkill) in this.InventoryLocalData.EquippedSkill)
            {
                if (valueSkill != id) continue;
                this.InventoryLocalData.EquippedSkill[keySlot] = null;
            }

            this.InventoryLocalData.EquippedSkill[slot] = id;
        }

        public void AddMoreSlot(uint count = 1)
        {
            this.InventoryLocalData.UnlockedSkillSlot += count;
        }
    }
}