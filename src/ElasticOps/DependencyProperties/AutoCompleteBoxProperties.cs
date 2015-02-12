using System;
using System.Windows;
using System.Windows.Controls;

namespace ElasticOps.DependencyProperties
{
    public class AutoCompleteBoxProperties : DependencyObject
    {
        public static readonly DependencyProperty SelectedIndexProperty = DependencyProperty.RegisterAttached("SelectedIndex", typeof(string), typeof(AutoCompleteBox));

        public static bool GetSelectedIndex(DependencyObject obj)
        {
            if (obj==null)
                throw new ArgumentNullException("obj");

            return (bool)obj.GetValue(SelectedIndexProperty);
        }

        public static void SetSelectedIndex(DependencyObject obj, bool value)
        {
            if (obj == null)
                throw new ArgumentNullException("obj");

            obj.SetValue(SelectedIndexProperty, value);
        }

    }
}
