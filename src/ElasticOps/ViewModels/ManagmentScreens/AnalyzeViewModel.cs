using System.Collections.Generic;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com;
using ElasticOps.Extensions;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(30)]
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


        public string CurrentEndpoint
        {
            get
            {
                NotifyOfPropertyChange(() => CanAnalyze);

                if (IsAnalyzerModeSelected && string.IsNullOrEmpty(IndexName))
                    return string.Format("/_analyze?analyzer={0}", string.IsNullOrEmpty(AnalyzerName) ? "[missing analyzer name]" : AnalyzerName);

                if (IsAnalyzerModeSelected && !string.IsNullOrEmpty(IndexName))
                    return string.Format("/{0}/_analyze?analyzer={1}",IndexName, string.IsNullOrEmpty(AnalyzerName) ? "[missing index name]" : AnalyzerName);

                if (IsFieldModeSlected )
                    return string.Format("/{0}/_analyze?field={1}", string.IsNullOrEmpty(IndexName) ? "[missing index name]" : IndexName, string.IsNullOrEmpty(FieldName) ? "[missing field name]" : FieldName);

                return "";
            }
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
                NotifyOfPropertyChange(() => CurrentEndpoint);
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
                NotifyOfPropertyChange(() => CurrentEndpoint);
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
                NotifyOfPropertyChange(() => CurrentEndpoint);
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
                NotifyOfPropertyChange(() => CurrentEndpoint);
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
                NotifyOfPropertyChange(() => CurrentEndpoint);
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
                NotifyOfPropertyChange(() => CurrentEndpoint);
            }
        }

        public IObservableCollection<AnalyzedToken> Tokens { get; set; }

        public void Analyze()
        {
            if (IsAnalyzerModeSelected && !string.IsNullOrEmpty(IndexName))
                AnalyzeWithIndexAnalyzer();

            if (IsFieldModeSlected)
                AnalyzeWithFieldAnalyzer();

            if (IsAnalyzerModeSelected && string.IsNullOrEmpty(IndexName))
                AnalyzeWithClusterAnalyzer();
        }

        private void AnalyzeWithClusterAnalyzer()
        {
            Analyze(new Analyze.AnalyzeCommand(_infrastructure.Connection, AnalyzerName, Text));
        }

        private void AnalyzeWithFieldAnalyzer()
        {
            Analyze(new Analyze.AnalyzeWithFieldAnalyzerCommand(_infrastructure.Connection, IndexName,FieldName,Text));
        }

        private void AnalyzeWithIndexAnalyzer()
        {
            Analyze(new Analyze.AnalyzeWithIndexAnalyzerCommand(_infrastructure.Connection, IndexName, AnalyzerName, Text));
        }

        private void Analyze<T>(T command) where T : Command<IEnumerable<AnalyzedToken>>
        {
            Tokens.Clear();

            var tokens =
                _infrastructure.CommandBus.Execute(command);

            if (tokens.Failed) return;

            tokens.Result.ForEach(x => Tokens.Add(x));
        }

    }
}
