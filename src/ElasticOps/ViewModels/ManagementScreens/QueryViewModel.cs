using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.ViewModels.Controls;
using ElasticOps.ViewModels.ManagementScreens;
using Newtonsoft.Json;

namespace ElasticOps.ViewModels
{
    [Priority(30)]
    public class QueryViewModel : Screen, IManagementScreen
    {
        private CodeEditorViewModel _queryEditor;
        private CodeEditorViewModel _resultEditor;
        private readonly Infrastructure _infrastructure;
        private string _url;

        public QueryViewModel(CodeEditorViewModel queryEditorModel, CodeEditorViewModel resultEditorModel, Infrastructure infrastructure)
        {
            base.DisplayName = "Query";
            _queryEditor = queryEditorModel;
            _resultEditor = resultEditorModel;
            _infrastructure = infrastructure;
            _resultEditor.IsReadOnly = true;

            Methods = new BindableCollection<ComboBoxItemViewModel>
            {
                 ComboBoxItemViewModel.FromString("GET"),
                 ComboBoxItemViewModel.FromString("POST"),
                 ComboBoxItemViewModel.FromString("PUT"),
                 ComboBoxItemViewModel.FromString("DELETE"),
                 ComboBoxItemViewModel.FromString("HEAD"),
                  
            };
            Method = "GET";
            Url = _infrastructure.Config.DefaultQueryUrl;

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
            }
        }

        private ObservableCollection<ComboBoxItemViewModel> _Methods;

        public ObservableCollection<ComboBoxItemViewModel> Methods
        {
            get { return _Methods; }
            private set
            {
                _Methods = value;
                NotifyOfPropertyChange(() => Methods);
            }
        }


        private string _Method;
        private bool _isExecuting;

        public string Method
        {
            get { return _Method; }
            set
            {
                _Method = value;
                NotifyOfPropertyChange(() => Method);
            }
        }


        public void Execute()
        {
            IsExecuting = true;
            Task.Factory.StartNew(ExecuteCall)
                .ContinueWith((t) => IsExecuting = false);

        }

        private void ExecuteCall()
        {
            try
            {
                var requestUri = _infrastructure.Connection.ClusterUri;
                if (!string.IsNullOrEmpty(Url))
                {
                    requestUri = new Uri(_infrastructure.Connection.ClusterUri, new Uri(Url, UriKind.Relative));
                }
                var request = WebRequest.Create(requestUri);
                var body = QueryEditor.Code;
                request.Method = !string.IsNullOrEmpty(body) && Method == "GET" ? "POST" : Method;
                if (!string.IsNullOrEmpty(body))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(body);
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                WebResponse response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());

                var originalJson = reader.ReadToEnd();

                ResultEditor.Code = TryFormatJson(originalJson);
            }
            catch (Exception ex)
            {
                ResultEditor.Code = ex.ToString();
            }
        }

        private static string TryFormatJson(string originalJson)
        {
            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(originalJson);
                var json = JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
                return json;
            }
            catch { }

            return originalJson;
        }

        public void FormatCode()
        {
            QueryEditor.Code = TryFormatJson(QueryEditor.Code);
        }

        public bool IsExecuting
        {
            get { return _isExecuting; }
            set
            {
                if (value.Equals(_isExecuting)) return;
                _isExecuting = value;
                NotifyOfPropertyChange(() => IsExecuting);
            }
        }

        public void KeyPress(KeyEventArgs args)
        {
            if (args.Key == Key.F5)
            {
                ExecuteCall();
            }

            if (args.Key == Key.R && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                FormatCode();
            }
        }
    }
}
