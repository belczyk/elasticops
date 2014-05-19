using System;


namespace ElasticOps
{
    public class IntSettingConstraintsAttribute : Attribute
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
