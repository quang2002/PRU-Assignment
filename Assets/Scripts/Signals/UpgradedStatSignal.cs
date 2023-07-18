namespace Signals
{
    using Models.Common;

    public class UpgradedStatSignal
    {
        public StatType StatType { get; init; }
        public uint     Level    { get; init; }
    }
}