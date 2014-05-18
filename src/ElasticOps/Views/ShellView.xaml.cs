
using System.Windows;
using ElasticOps.ViewModels;
using MahApps.Metro.Controls;

namespace ElasticOps.Views
{

    public partial class ShellView : MetroWindow
    {
        public ShellView()
        {
            InitializeComponent();
            DialogManager.Window = this;
        }

    }
}
