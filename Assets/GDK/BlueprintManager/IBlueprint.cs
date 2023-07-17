namespace GDK.BlueprintManager
{
    public interface IBlueprint
    {
        public string AddressableKey { get; }
        void          LoadBlueprint();
    }
}