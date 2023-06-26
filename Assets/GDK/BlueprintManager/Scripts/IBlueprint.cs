namespace GDK.BlueprintManager.Scripts
{

    public interface IBlueprint
    {
        public string AddressableKey { get; }
        void          LoadBlueprint();
    }

}