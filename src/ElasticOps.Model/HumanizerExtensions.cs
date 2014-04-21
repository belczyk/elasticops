using System;

namespace ElasticOps
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

        public static string HumanizePath(this string path)
        {
            if (string.IsNullOrEmpty(path)) return path;

            return path.Replace("/", "\\");
        }
    }
}
