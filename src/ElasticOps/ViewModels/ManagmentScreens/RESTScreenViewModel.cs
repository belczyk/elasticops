using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.ViewModels.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(40)]
    public class RESTScreenViewModel : Screen, IManagmentScreen
    {
        private readonly Infrastructure _infrastructure;
        private IEventAggregator _eventAggregator;

        public RESTScreenViewModel(Infrastructure infrastructure, CodeEditorViewModel rquestBodyViewModel, CodeEditorViewModel resultViewModel)
        {
            ResultEditor = resultViewModel;
            RequestBodyEditor = rquestBodyViewModel;

            _infrastructure = infrastructure;
            DisplayName = "REST";
            _eventAggregator = infrastructure.EventAggregator;

            Methods = new List<ComboBoxItemViewModel>
            {
                 ComboBoxItemViewModel.FromString("GET"),
                 ComboBoxItemViewModel.FromString("POST"),
                 ComboBoxItemViewModel.FromString("PUT"),
                 ComboBoxItemViewModel.FromString("DELETE"),
                 ComboBoxItemViewModel.FromString("HEAD"),
                 ComboBoxItemViewModel.FromString("OPTIONS"),
                 ComboBoxItemViewModel.FromString("TRACE"),
                 ComboBoxItemViewModel.FromString("PATCH"),
            };
            Method = "GET";
        }


        public IEnumerable<ComboBoxItemViewModel> Methods { get; set; }

        public CodeEditorViewModel ResultEditor { get; set; }
        public CodeEditorViewModel RequestBodyEditor { get; set; }

        private string _Method;
        public string Method
        {
            get { return _Method; }
            set
            {
                _Method = value;
                NotifyOfPropertyChange(() => Method);
            }
        }

        private string _Endpoint;
        public string Endpoint
        {
            get { return _Endpoint; }
            set
            {
                _Endpoint = value;
                NotifyOfPropertyChange(() => Endpoint);
            }
        }

        private bool _IsExecuting;
        public bool IsExecuting
        {
            get { return _IsExecuting; }
            set
            {
                _IsExecuting = value;
                NotifyOfPropertyChange(() => IsExecuting);
            }
        }

        public void Execute()
        {
            IsExecuting = true;
            var task = new Task(ExecuteCall);
            task.ContinueWith((t) => IsExecuting = false);
            task.Start();
        }

        private void ExecuteCall()
        {
            try
            {
                var requestUri = GetRequestUri();
                var request = WebRequest.Create(requestUri);
                request.Method = Method;
                if (!string.IsNullOrEmpty(RequestBodyEditor.Code))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(RequestBodyEditor.Code);
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                WebResponse response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());

                ResultEditor.Code = TryFormatIfJson(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                ResultEditor.Code = ex.Message;
            }
        }

        private Uri GetRequestUri()
        {
            var requestUri = _infrastructure.Connection.ClusterUri;
            if (!string.IsNullOrEmpty(Endpoint))
            {
                Uri tmpUri;
                var uri = Uri.TryCreate(Endpoint, UriKind.Absolute, out tmpUri);

                requestUri = uri ? tmpUri : new Uri(_infrastructure.Connection.ClusterUri, new Uri(Endpoint, UriKind.Relative));
            }
            return requestUri;
        }

        public void OnEndpointKeyDown(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                Execute();
            }
        }

        private string TryFormatIfJson(string response)
        {
            try
            {
                dynamic obj = JObject.Parse(response);
                return JsonConvert.SerializeObject(obj, Formatting.Indented);
            }
            catch
            {
                return response;
            }

        }

        protected override void OnActivate()
        {
            _eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            _eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }


        public void KeyPress(KeyEventArgs args)
        {
            if (args.Key == Key.F5)
            {
                ExecuteCall();
            }

            if (args.Key == Key.R && Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
            {
                RequestBodyEditor.Code = TryFormatIfJson(RequestBodyEditor.Code);
            }
        }
    }
}
