using System;
using System.Globalization;

namespace ElasticOps.Extensions
{
    public static class LongExtensions
    {
        public static string Humanize(this long number)
        {
            if (number == 0) return "0";

            double exponent = (long) Math.Log10(Math.Abs(number));
            switch ((long) Math.Floor(exponent))
            {
                case 0:
                case 1:
                case 2:
                    return number.ToString(CultureInfo.InvariantCulture);
                case 3:
                case 4:
                case 5:
                    return (number/1e3).ToString("F1", CultureInfo.InvariantCulture) + "K";
                case 6:
                case 7:
                case 8:
                    return (number/1e6).ToString("F1", CultureInfo.InvariantCulture) + "M";
                case 9:
                case 10:
                case 11:
                    return (number/1e9).ToString("F1", CultureInfo.InvariantCulture) + "G";
                case 12:
                case 13:
                case 14:
                    return (number/1e12).ToString("F1", CultureInfo.InvariantCulture) + "T";
                case 15:
                case 16:
                case 17:
                    return (number/1e15).ToString("F1", CultureInfo.InvariantCulture) + "P";
                case 18:
                case 19:
                case 20:
                    return (number/1e18).ToString("F1", CultureInfo.InvariantCulture) + "E";
                case 21:
                case 22:
                case 23:
                    return (number/1e21).ToString("F1", CultureInfo.InvariantCulture) + "Z";
                default:
                    return (number/1e24).ToString("F1", CultureInfo.InvariantCulture) + "Y";
            }
        }
    }
}