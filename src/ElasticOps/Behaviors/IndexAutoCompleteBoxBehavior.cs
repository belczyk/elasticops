using ElasticOps.Behaviors.Suggesters;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ElasticOps.Behaviors
{
    public class IndexAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        private IndexSuggestCollection _indexSuggestCollection;

        protected override void OnAttached()
        {
            AssociatedObject.ItemTemplate = Application.Current.FindResource("UrlAutoCompleteBoxItemTemplate") as DataTemplate;
            AssociatedObject.IsTextCompletionEnabled = true;
            AssociatedObject.MinimumPrefixLength = 0;

            _indexSuggestCollection = AppBootstrapper.GetInstance<IndexSuggestCollection>();

            AssociatedObject.ItemsSource = _indexSuggestCollection;
            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
        {
            _indexSuggestCollection.UpdateSuggestions(AssociatedObject.Text);
        }

    }
}
