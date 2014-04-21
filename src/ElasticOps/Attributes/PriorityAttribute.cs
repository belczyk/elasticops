
using System;

namespace ElasticOps.Attributes
{

    public class PriorityAttribute : Attribute
    {
        public int Priority { get; set; }

        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }


    }
}
