using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Behaviors;
using ElasticOps.ViewModels.Controls;
using ElasticOps.ViewModels.ManagmentScreens;

namespace ElasticOps.ViewModels
{
    [Priority(1)]
    public class QueryViewModel : Screen, IManagmentScreen
    {
        private Infrastructure _infrastructure;
        private CodeEditorViewModel _queryEditor;
        private CodeEditorViewModel _resultEditor;
        private string _url;

        public QueryViewModel(Infrastructure infrastructure, CodeEditorViewModel queryEditorModel, CodeEditorViewModel resultEditorModel)
        {
            DisplayName = "Query";
            _infrastructure = infrastructure;
            _queryEditor = queryEditorModel;
            _resultEditor = resultEditorModel;
            UrlSuggest = new IndexSuggest(infrastructure);

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


        public string Url
        {
            get { return _url; }
            set
            {
                if (value == _url) return;
                _url = value;
                NotifyOfPropertyChange(() => Url);
                UrlSuggest.UpdateSuggestions(_url);
            }
        }

        public IndexSuggest UrlSuggest { get; set; }
    }
}
