using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.ViewModels.Controls;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    [Priority(2)]
    class QueryViewModel : Screen, IManagmentScreen
    {
        private Infrastructure infrastructure;
        private CodeEditorViewModel _queryEditor;
        private CodeEditorViewModel _resultEditor;

        public QueryViewModel(Infrastructure infrastructure, CodeEditorViewModel queryEditorModel, CodeEditorViewModel resultEditorModel)
        {
            DisplayName = "Query";
            this.infrastructure = infrastructure;
            _queryEditor = queryEditorModel;
            _resultEditor = resultEditorModel;
            UrlSuggest = new ObservableCollection<string>();

        }

        public CodeEditorViewModel QueryEditor
        {
            get { return _queryEditor; }
            set
            {
                if (Equals(value, _queryEditor)) return;
                _queryEditor = value;
                NotifyOfPropertyChange(() => QueryEditor);
            }
        }

        public CodeEditorViewModel ResultEditor
        {
            get { return _resultEditor; }
            set
            {
                if (Equals(value, _resultEditor)) return;
                _resultEditor = value;
                NotifyOfPropertyChange(() => ResultEditor);
            }
        }

        public ObservableCollection<string> UrlSuggest { get; set; }
    }
}
