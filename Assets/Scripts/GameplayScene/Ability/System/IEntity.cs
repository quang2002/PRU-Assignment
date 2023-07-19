namespace GameplayScene.Ability.System
{
    using global::System.Collections.Generic;

    public interface IEntity
    {
        public List<BaseEffect> Effects { get; }

        public float Health { get; set; }
    }

    public static class EntityExtensions
    {
        public static bool IsAlive(this IEntity entity)
        {
            return entity.Health > 0;
        }
    }
}