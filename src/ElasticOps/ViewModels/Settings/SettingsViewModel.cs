﻿using Caliburn.Micro;
using ElasticOps.Com.Events;
using ElasticOps.Extensions;

namespace ElasticOps.ViewModels
{
    public class SettingsViewModel : Conductor<SettingPageViewModel>.Collection.OneActive
    {
        private Infrastructure infrastructure;
        private SettingToViewModelMapper settingToViewModelMapper = new SettingToViewModelMapper();

        public SettingsViewModel(Infrastructure infrastructure)
        {
            this.infrastructure = infrastructure;
            SettingsPages = new BindableCollection<SettingPageViewModel>();

            LoadSettingsPages();
        }

        private void LoadSettingsPages()
        {
            var userSettings = AppBootstrapper.GetAllInstances<UserSettings>().OrderByPriority();

            foreach (var us in userSettings)
            {
                SettingsPages.Add(new SettingPageViewModel
                {
                    DisplayName = us.DisplayName,
                    UserSettings = us,
                    Settings = settingToViewModelMapper.GetSettingsViewModels(us)
                });
            }
        }

          
        public BindableCollection<SettingPageViewModel> SettingsPages { get; set; }

    }
}