using Caliburn.Micro;
using ElasticOps.Com;

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


        public void Handle(GoToStudioEvent message)
        {
            ActivateItem(studioViewModel);
        }

    }
}
