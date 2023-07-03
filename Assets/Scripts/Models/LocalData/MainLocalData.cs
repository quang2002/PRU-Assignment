namespace Models.LocalData
{
    using System.Numerics;
    using GDK.LocalData.Scripts;

    public class MainLocalData : ILocalData
    {
        public string      Username    { get; set; }
        public BigInteger  Coins       { get; set; }
        public UpgradeData UpgradeData { get; set; }

        public void Initialize()
        {
            this.Username    = "Merlin the Wizard";
            this.Coins       = 0;
            this.UpgradeData = default;
        }
    }
}