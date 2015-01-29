using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace ElasticOps.Behaviors
{
    public class UrlAutoCompleteBoxBehavior : Behavior<AutoCompleteBox>
    {
        protected override void OnAttached()
        {
            AssociatedObject.ItemTemplate = Application.Current.FindResource("UrlAutoCompleteBoxItemTemplate") as DataTemplate;
            var urlSuggest = new UrlSuggest(AppBootstrapper.GetInstance<Infrastructure>());

            AssociatedObject.ItemsSource = urlSuggest;

            AssociatedObject.TextChanged += (s,e) => urlSuggest.UpdateSuggestions(AssociatedObject.Text);
        }

    }
}
