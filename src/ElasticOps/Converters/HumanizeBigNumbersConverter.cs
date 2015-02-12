using System;
using System.Globalization;
using System.Windows.Data;
using ElasticOps.Extensions;

namespace ElasticOps.Converters
{
    public class HumanizeBigNumbersConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return ((int) value).Humanize();
            if (value is long)
                return ((long) value).Humanize();

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}