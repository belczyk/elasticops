using System;
using System.Globalization;
using System.Windows.Data;

namespace ElasticOps.Converters
{
    public class HumanizeBigNumbersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var d = (int)value;

            double exponent = (int)Math.Log10(Math.Abs(d));
            switch ((int)Math.Floor(exponent))
            {
                case 0:
                case 1:
                case 2:
                    return d;
                case 3:
                case 4:
                case 5:
                    return (d / 1e3).ToString("F1") + "K";
                case 6:
                case 7:
                case 8:
                    return (d / 1e6).ToString("F1") + "M";
                case 9:
                case 10:
                case 11:
                    return (d / 1e9).ToString("F1") + "G";
                case 12:
                case 13:
                case 14:
                    return (d / 1e12).ToString("F1") + "T";
                case 15:
                case 16:
                case 17:
                    return (d / 1e15).ToString("F1") + "P";
                case 18:
                case 19:
                case 20:
                    return (d / 1e18).ToString("F1") + "E";
                case 21:
                case 22:
                case 23:
                    return (d / 1e21).ToString("F1") + "Z";
                default:
                    return (d / 1e24).ToString("F1") + "Y";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}