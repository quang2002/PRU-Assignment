namespace GDK.Services.Unity
{
    public class ApplicationPauseSignal
    {
        public bool PauseStatus { get; internal set; }
    }

    public class ApplicationFocusSignal
    {
        public bool HasFocus { get; internal set; }
    }

    public class ApplicationQuitSignal
    {
    }
}