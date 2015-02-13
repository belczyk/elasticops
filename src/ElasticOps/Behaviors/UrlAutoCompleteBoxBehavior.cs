using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using ElasticOps.Behaviors.AutoComplete;

namespace ElasticOps.Behaviors
{
    public class UrlAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        private UrlAutoCompleteCollection _urlAutoCompleteCollection;

        protected override void OnAttached()
        {
            AssociatedObject.ItemTemplate =
                Application.Current.FindResource("UrlAutoCompleteBoxItemTemplate") as DataTemplate;
            _urlAutoCompleteCollection = AppBootstrapper.GetInstance<UrlAutoCompleteCollection>();
            AssociatedObject.IsTextCompletionEnabled = true;
            AssociatedObject.MinimumPrefixLength = 0;
            AssociatedObject.ItemsSource = _urlAutoCompleteCollection;

            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
        {
            _urlAutoCompleteCollection.UpdateSuggestions(AssociatedObject.Text);
        }
    }
}