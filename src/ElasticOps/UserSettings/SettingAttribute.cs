using System;

namespace ElasticOps
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false)]
    public class SettingAttribute : Attribute
    {
        private int priority;
        private string displayName;

        public SettingAttribute(string displayName)
        {
            this.displayName = displayName;
        }

        public SettingAttribute(string displayName, int priority)
        {
            this.displayName = displayName;
            this.priority = priority;
        }

        public int Priority { get { return priority; } }
        public string DisplayName { get { return displayName; } }
    }
}
