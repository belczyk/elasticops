using System;
using Caliburn.Micro;
using ElasticOps.Attributes;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(2)]
    public class RESTScreenViewModel : Screen, IManagmentScreen
    {
        public RESTScreenViewModel()
        {
            DisplayName = "REST";
        }

    }
}
