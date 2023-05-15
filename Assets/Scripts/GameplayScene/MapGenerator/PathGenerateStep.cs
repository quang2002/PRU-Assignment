namespace GameplayScene.MapGenerator
{
    public class PathGenerateStep : IMapGenerateStep
    {
        public MapGeneratorSettings Settings { get; set; }
        public dynamic              Input    { get; set; }
        public dynamic              Output   { get; set; }

        public void Execute()
        {
            this.Output = this.Input;
        }
    }
}