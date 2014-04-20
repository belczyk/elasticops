using System;

namespace ElasticOps.Model
{
    public static class HumanizerExtensions
    {
        public static string HumanizeNumber(this long number)
        {
            if(number >= 1000000)
                return Convert.ToString(string.Format("{0} M", number / 1000000));
            if (number >= 1000)
                return Convert.ToString(string.Format("{0} K", number / 1000));
            return Convert.ToString(number);
        }
    }
}
