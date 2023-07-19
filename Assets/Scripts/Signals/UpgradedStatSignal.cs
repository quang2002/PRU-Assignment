namespace Signals
{
    using Common;

    public class UpgradedStatSignal
    {
        public StatType StatType { get; init; }
        public long     Level    { get; init; }
    }
}