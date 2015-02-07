using ElasticOps.Behaviors.Suggesters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ElasticOps.Behaviors
{
    public class IndexAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        private IndexSuggest _indexSuggest;

        protected override void OnAttached()
        {
            AssociatedObject.ItemTemplate = Application.Current.FindResource("UrlAutoCompleteBoxItemTemplate") as DataTemplate;
            AssociatedObject.IsTextCompletionEnabled = true;
            AssociatedObject.MinimumPrefixLength = 0;

            _indexSuggest = AppBootstrapper.GetInstance<IndexSuggest>();

            AssociatedObject.ItemsSource = _indexSuggest;
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
        {
            _indexSuggest.UpdateSuggestions(AssociatedObject.Text);
        }

    }
}
