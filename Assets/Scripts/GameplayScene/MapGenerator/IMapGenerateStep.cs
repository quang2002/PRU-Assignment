namespace GameplayScene.MapGenerator
{
    using System.Dynamic;

    public interface IMapGenerateStep
    {
        public MapGeneratorSettings Settings { get; set; }
        public dynamic              Input    { get; set; }
        public dynamic              Output   { get; set; }

        public void Execute();
    }

    public static class GenerateMapUtils
    {
        public static T UseStep<T>(this MapGenerator mapGenerator)
            where T : class, IMapGenerateStep, new()
        {
            var stepToUse = new T
            {
                Settings = mapGenerator.Settings,
                Input    = null,
                Output   = new ExpandoObject()
            };

            stepToUse.Execute();

            return stepToUse;
        }

        public static T UseStep<T>(this IMapGenerateStep step)
            where T : class, IMapGenerateStep, new()
        {
            var stepToUse = new T
            {
                Settings = step.Settings,
                Input    = step.Output,
                Output   = new ExpandoObject()
            };

            stepToUse.Execute();

            return stepToUse;
        }
    }
}