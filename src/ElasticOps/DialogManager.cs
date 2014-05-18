
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ElasticOps
{
    public class DialogManager
    {
        public static MetroWindow Window { get; set; }

        public async void ShowMessage(string title, string message)
        {
            await Window.ShowMessageAsync(title, message,MessageDialogStyle.Affirmative,new MetroDialogSettings
            {
                AffirmativeButtonText = "OK"
            });

        }
    }
}
