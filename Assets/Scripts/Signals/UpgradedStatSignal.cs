namespace Signals
{
    using Models.Common;

    public class UpgradedStatSignal
    {
        public StatType StatType { get; init; }
        public long     Level    { get; init; }
    }
}