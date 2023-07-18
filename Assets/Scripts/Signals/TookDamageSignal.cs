namespace Signals
{
    using GameplayScene.Ability.System;

    public class TookDamageSignal
    {
        public IEntity Entity { get; init; }
        public long    Damage { get; init; }
    }
}