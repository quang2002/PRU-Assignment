namespace Signals
{
    public class CoinChangedSignal
    {
        public long OldCoins { get; init; }
        public long NewCoins { get; init; }

        public bool IsIncreased => this.NewCoins > this.OldCoins;
        public bool IsDecreased => this.NewCoins < this.OldCoins;
    }
}