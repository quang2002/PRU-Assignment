namespace Models.LocalData
{
    using GDK.LocalData;

    public class MainLocalData : ILocalData
    {
        public string      Username    { get; set; }
        public long        Coins       { get; set; }
        public UpgradeData UpgradeData { get; set; }
        public uint        Exp         { get; set; }

        public void Initialize()
        {
            this.Username    = "Merlin the Wizard";
            this.Coins       = 0;
            this.Exp         = 0;
            this.UpgradeData = default;
        }
    }
}