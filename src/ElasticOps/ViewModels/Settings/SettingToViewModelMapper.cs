using System.Linq;
using System.Reflection;
using Caliburn.Micro;

namespace ElasticOps.ViewModels
{
    public class SettingToViewModelMapper
    {
        public IObservableCollection<SettingViewModel> GetSettingsViewModels(UserSettings userSettings)
        {
            var settingProperties =
                userSettings.GetType()
                    .GetProperties()
                    .Where(x => x.GetCustomAttributes(typeof(SettingAttribute)).Any());
            var ret = new BindableCollection<SettingViewModel>();
            settingProperties.Select(GetViewModel).Where(x => x != null).ToList().ForEach(ret.Add);

            return ret;
        }

        private SettingViewModel GetViewModel(PropertyInfo settingProperty)
        {
            var settingAttribute = settingProperty.GetCustomAttributes(typeof(SettingAttribute)).Single() as SettingAttribute;

            var viewModel = GetViewModelFor(settingProperty);

            if (viewModel == null) return null;

            viewModel.DisplayName = settingAttribute.DisplayName;
            viewModel.Proiority = settingAttribute.Priority;

            return viewModel;
        }

        private SettingViewModel GetViewModelFor(PropertyInfo propertyInfo)
        {
            switch (propertyInfo.PropertyType.Name.ToLower())
            {
                case "string":
                    return AppBootstrapper.GetInstance<StringSettingViewModel>();
                case "uri":
                    return AppBootstrapper.GetInstance<UriSettingViewModel>();
                case "int32":
                    var intVM = AppBootstrapper.GetInstance<IntSettingViewModel>();
                    SetIntConstraints(propertyInfo, intVM);
                    return intVM;
            }

            return null;
        }

        private void SetIntConstraints(PropertyInfo propertyInfo, IntSettingViewModel intVM)
        {
            var constraintAttr = propertyInfo.GetAttributes<IntSettingConstraintsAttribute>(true).SingleOrDefault();
            if (constraintAttr == null) return;

            intVM.Maximum = constraintAttr.Maximum;
            intVM.Minimum = constraintAttr.Minimum;
        }
    }
}
