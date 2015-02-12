using System;

namespace ElasticOps
{
    public sealed class IntSettingConstraintsAttribute : Attribute
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public IntSettingConstraintsAttribute(int minimum, int maximum)
        {
            Minimum = minimum;
            Maximum = maximum;
        }
    }
}
