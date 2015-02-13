using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using ElasticOps.Behaviors.AutoComplete;

namespace ElasticOps.Behaviors
{
    public class IndexAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        private IndexAutoCompleteCollection _indexAutocompleteCollection;

        protected override void OnAttached()
        {
            AssociatedObject.ItemTemplate =
                Application.Current.FindResource("UrlAutoCompleteBoxItemTemplate") as DataTemplate;
            AssociatedObject.IsTextCompletionEnabled = true;
            AssociatedObject.MinimumPrefixLength = 0;

            _indexAutocompleteCollection = AppBootstrapper.GetInstance<IndexAutoCompleteCollection>();

            AssociatedObject.ItemsSource = _indexAutocompleteCollection;
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        private void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
        {
            _indexAutocompleteCollection.UpdateSuggestions(AssociatedObject.Text);
        }
    }
}