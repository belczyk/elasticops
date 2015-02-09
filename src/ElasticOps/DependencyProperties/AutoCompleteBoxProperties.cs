using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ElasticOps.DependencyProperties
{
    public class AutoCompleteBoxProperties : DependencyObject
    {
        public static DependencyProperty SelectedIndexProperty = DependencyProperty.RegisterAttached("SelectedIndex", typeof(string), typeof(AutoCompleteBox));

        public static bool GetSelectedIndex(DependencyObject obj)
        {
            return (bool)obj.GetValue(SelectedIndexProperty);
        }

        public static void SetSelectedIndex(DependencyObject obj, bool value)
        {
            obj.SetValue(SelectedIndexProperty, value);
        }

    }
}
