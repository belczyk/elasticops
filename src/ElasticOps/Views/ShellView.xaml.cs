using System.Windows;
using MahApps.Metro.Controls;

namespace ElasticOps.Views
{
    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();

            this.Closed += ShellView_Closed;
        }

        private void ShellView_Closed(object sender, System.EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}