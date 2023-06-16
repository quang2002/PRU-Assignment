namespace GDK.UIManager.Scripts
{
    using System;

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ScreenInfoAttribute : Attribute
    {
        public ScreenInfoAttribute(string id)
        {
            this.ID = id;
        }

        public string ID { get; set; }
    }
}