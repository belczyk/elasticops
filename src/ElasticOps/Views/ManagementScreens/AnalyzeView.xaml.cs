using System.Windows.Controls;
using ElasticOps.Commands;

namespace ElasticOps.Views.ManagementScreens
{
    /// <summary>
    /// Interaction logic for AnalyzeView.xaml
    /// </summary>
    public partial class AnalyzeView : UserControl
    {
        public AnalyzeView()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TokensGrid.SelectedItem != null)
            {
                var token = TokensGrid.SelectedItem as AnalyzedToken;

                if (token == null) return;

                Text.Focus();
                Text.CaretIndex = token.StartOffset;
                Text.Select(token.StartOffset, token.EndOffset - token.StartOffset);

            }
        }
    }
}