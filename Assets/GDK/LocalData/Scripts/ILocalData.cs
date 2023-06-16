namespace GDK.LocalData.Scripts
{
    using System.ComponentModel;

    public interface ILocalData : INotifyPropertyChanged
    {
        internal void Initialize();
    }
}