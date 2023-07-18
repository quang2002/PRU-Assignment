namespace Models.LocalData
{
    using System;

    [Serializable]
    public struct UpgradeData
    {
        public long AttackLevel         { get; set; }
        public long HealthLevel         { get; set; }
        public long AttackSpeedLevel    { get; set; }
        public long CriticalRateLevel   { get; set; }
        public long CriticalDamageLevel { get; set; }
    }
}