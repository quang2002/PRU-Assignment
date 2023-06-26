namespace Models.LocalData
{

    using System;

    [Serializable]
    public class UpgradeData
    {
        public uint AttackLevel         { get; set; }
        public uint HealthLevel         { get; set; }
        public uint AttackSpeedLevel    { get; set; }
        public uint CriticalRateLevel   { get; set; }
        public uint CriticalDamageLevel { get; set; }
    }

}