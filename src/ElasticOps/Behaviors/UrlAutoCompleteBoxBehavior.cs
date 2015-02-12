using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using ElasticOps.Behaviors.Suggesters;

namespace ElasticOps.Behaviors
{
    public class UrlAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        private UrlSuggestCollection _urlSuggestCollection;

        protected override void OnAttached()
        {
            AssociatedObject.ItemTemplate = Application.Current.FindResource("UrlAutoCompleteBoxItemTemplate") as DataTemplate;
            _urlSuggestCollection = AppBootstrapper.GetInstance<UrlSuggestCollection>();
            AssociatedObject.IsTextCompletionEnabled = true;
            AssociatedObject.MinimumPrefixLength = 0;
            AssociatedObject.ItemsSource = _urlSuggestCollection;

            AssociatedObject.TextChanged += AssociatedObject_TextChanged;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.TextChanged -= AssociatedObject_TextChanged;
            base.OnDetaching();
        }

        void AssociatedObject_TextChanged(object sender, RoutedEventArgs e)
        {
            _urlSuggestCollection.UpdateSuggestions(AssociatedObject.Text);
        }

    }
}
