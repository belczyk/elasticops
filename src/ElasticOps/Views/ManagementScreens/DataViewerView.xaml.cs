using System.Windows.Controls;
using System.Windows.Input;
using Caliburn.Micro;
using ElasticOps.Commands;
using ElasticOps.Events;

namespace ElasticOps.Views.ManagementScreens
{
    /// <summary>
    /// Interaction logic for DataViewerView.xaml
    /// </summary>
    public partial class DataViewerView : UserControl
    {
        public DataViewerView()
        {
            InitializeComponent();
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var cell = (DataGridCell) sender;

            if (cell.Content is TextBlock)
                AppBootstrapper.GetInstance<IEventAggregator>()
                    .PublishOnUIThread(new PreviewValueEvent(((TextBlock) cell.Content).Text));
        }
    }
}