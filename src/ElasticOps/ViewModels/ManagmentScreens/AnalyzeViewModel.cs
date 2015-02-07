using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com;
using ElasticOps.Extensions;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(10)]
    public class AnalyzeViewModel : Conductor<object>, IManagmentScreen
    {
        private readonly Infrastructure _infrastructure;

        private string _text;
        private string _analyzerName;
        private bool _isAnalyzerModeSelected;
        private bool _isFieldModeSlected;
        private string _fieldName;
        private string _indexName;

        public AnalyzeViewModel(Infrastructure infrastructure)
        {
            _infrastructure = infrastructure;
            DisplayName = "Analyze";
            AnalyzerName = "standard";
            IsAnalyzerModeSelected = true;
            Tokens = new BindableCollection<AnalyzedToken>();
        }

        public bool CanAnalyze
        {
            get
            {
                return !string.IsNullOrEmpty(Text) &&
                       ((IsAnalyzerModeSelected && !string.IsNullOrEmpty(AnalyzerName)) ||
                        (IsFieldModeSlected && !string.IsNullOrEmpty(FieldName) && !string.IsNullOrEmpty(IndexName)));
            }
        }

        public bool IsAnalyzerModeSelected
        {
            get { return _isAnalyzerModeSelected; }
            set
            {
                if (value.Equals(_isAnalyzerModeSelected)) return;
                _isAnalyzerModeSelected = value;
                NotifyOfPropertyChange(() => IsAnalyzerModeSelected);
                NotifyOfPropertyChange(() => CanAnalyze);
            }
        }

        public bool IsFieldModeSlected
        {
            get { return _isFieldModeSlected; }
            set
            {
                if (value.Equals(_isFieldModeSlected)) return;
                _isFieldModeSlected = value;
                NotifyOfPropertyChange(() => IsFieldModeSlected);
                NotifyOfPropertyChange(() => CanAnalyze);
            }
        }

        public string AnalyzerName
        {
            get { return _analyzerName; }
            set
            {
                if (value == _analyzerName) return;
                _analyzerName = value;
                NotifyOfPropertyChange(() => AnalyzerName);
                NotifyOfPropertyChange(() => CanAnalyze);
            }
        }

        public string Text
        {
            get { return _text; }
            set
            {
                if (value == _text) return;
                _text = value;
                NotifyOfPropertyChange(() => Text);
                NotifyOfPropertyChange(() => CanAnalyze);
            }
        }

        public string FieldName
        {
            get { return _fieldName; }
            set
            {
                if (value == _fieldName) return;
                _fieldName = value;
                NotifyOfPropertyChange(() => FieldName);
                NotifyOfPropertyChange(() => CanAnalyze);
            }
        }

        public string IndexName
        {
            get { return _indexName; }
            set
            {
                if (value == _indexName) return;
                _indexName = value;
                NotifyOfPropertyChange(() => IndexName);
                NotifyOfPropertyChange(() => CanAnalyze);
            }
        }

        public IObservableCollection<AnalyzedToken> Tokens { get; set; }

        public void Analyze()
        {
            if (IsAnalyzerModeSelected && !string.IsNullOrEmpty(IndexName))
                AnalyzeUsingIndexAnalyzer();

            if (IsFieldModeSlected)
                AnalyzeIndexFieldAnalyzer();

            if (IsAnalyzerModeSelected && string.IsNullOrEmpty(IndexName))
                AnalyzeClusterAnalyzer();

            var tokens = _infrastructure.CommandBus.Execute(new Analyze.AnalyzeCommand(_infrastructure.Connection, AnalyzerName,Text));

            if (tokens.Failed) return;

            Tokens.Clear();

            tokens.Result.ForEach(x=>Tokens.Add(x));
        }

        private void AnalyzeClusterAnalyzer()
        {
            throw new System.NotImplementedException();
        }

        private void AnalyzeIndexFieldAnalyzer()
        {
            throw new System.NotImplementedException();
        }

        private void AnalyzeUsingIndexAnalyzer()
        {
            throw new System.NotImplementedException();
        }
    }
}
