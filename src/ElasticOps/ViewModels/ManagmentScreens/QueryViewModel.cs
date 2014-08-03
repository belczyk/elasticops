using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    [Priority(2)]
    class QueryViewModel : Screen, IManagmentScreen
    {
        private Infrastructure infrastructure;

        public QueryViewModel(Infrastructure infrastructure)
        {
            DisplayName = "Query";
            this.infrastructure = infrastructure;
        }


    }
}
