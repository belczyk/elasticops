using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using ElasticOps.Com;
using MahApps.Metro;

namespace ElasticOps.ViewModels
{
    public class ShellViewModel : Conductor<object>.Collection.OneActive, IHandle<GoToStudioEvent>
    {
     
        private StudioViewModel studioViewModel;
        private SettingsViewModel _settingsViewModel;

        public ShellViewModel( 
            Infrastructure infrastructure,
            StudioViewModel studioViewModel, 
            SettingsViewModel _settingsViewModel,
            FooterViewModel footerViewModel
            )
        {
            this.studioViewModel = studioViewModel;
            this._settingsViewModel = _settingsViewModel;
            FooterViewModel = footerViewModel;

            DisplayName = "Elastic Ops";

            ActivateItem(studioViewModel);
            infrastructure.EventAggregator.Subscribe(this);


            AccentColors = ThemeManager.Accents
                                            .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                            .ToList();

            AppThemes = ThemeManager.AppThemes
                                           .Select(a => new AppThemeMenuData() { Name = a.Name, BorderColorBrush = a.Resources["BlackColorBrush"] as Brush, ColorBrush = a.Resources["WhiteColorBrush"] as Brush })
                                           .ToList();

        }
        private FooterViewModel _footerViewModel;

        public FooterViewModel FooterViewModel
        {
            get { return _footerViewModel; }
            set
            {
                _footerViewModel = value;
                NotifyOfPropertyChange(() => FooterViewModel);
            }
        }

        public void ShowSettings()
        {
            ActivateItem(_settingsViewModel);
        }
        public void ShowStudio()
        {
            ActivateItem(studioViewModel);
        }

        public void SwitchToDarkTheme()
        {
            var accent = ThemeManager.Accents.First(x => x.Name == "Blue");
            var theme = ThemeManager.GetAppTheme("BaseDark");
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
        }

        public void SwitchToLightTheme()
        {
            var accent = ThemeManager.Accents.First(x => x.Name == "Blue");
            var theme = ThemeManager.GetAppTheme("BaseLight");
            ThemeManager.ChangeAppStyle(Application.Current, accent, theme);
        }

        public List<AccentColorMenuData> AccentColors { get; set; }
        public List<AppThemeMenuData> AppThemes { get; set; }


        public void Handle(GoToStudioEvent message)
        {
            ActivateItem(studioViewModel);
        }

    }
}
