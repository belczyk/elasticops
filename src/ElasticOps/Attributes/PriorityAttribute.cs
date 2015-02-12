using System;

namespace ElasticOps.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public sealed class PriorityAttribute : Attribute
    {
        public int Priority { get; private set; }

        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}