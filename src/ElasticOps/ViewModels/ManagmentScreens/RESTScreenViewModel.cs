using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Events;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    [Priority(2)]
    public class RESTScreenViewModel : Screen, IManagmentScreen, IHandle<ClusterUriChanged>
    {
        private IEventAggregator eventAggregator;

        public RESTScreenViewModel(Settings settings, IEventAggregator eventAggregator)
        {
            DisplayName = "REST";
            ClusterUri = settings.ClusterUri;
            this.eventAggregator = eventAggregator;

            Methods = new BindableCollection<ComboBoxItemViewModel>
            {
                 ComboBoxItemViewModel.FromString("GET"),
                 ComboBoxItemViewModel.FromString("POST"),
                 ComboBoxItemViewModel.FromString("PUT"),
                 ComboBoxItemViewModel.FromString("DELETE"),
                 ComboBoxItemViewModel.FromString("HEAD"),
                  
            };
            Method = "GET";
        }

        private BindableCollection<ComboBoxItemViewModel> _Methods;

        public BindableCollection<ComboBoxItemViewModel> Methods
        {
            get { return _Methods; }
            set
            {
                _Methods = value;
                NotifyOfPropertyChange(() => Methods);
            }
        }


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

        private string _RequestBody;

        public string RequestBody
        {
            get { return _RequestBody; }
            set
            {
                _RequestBody = value;
                NotifyOfPropertyChange(() => RequestBody);
            }
        }

        private string _Response;

        public string Response
        {
            get { return _Response; }
            set
            {
                _Response = value;
                NotifyOfPropertyChange(() => Response);
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
                var requestUri = ClusterUri;
                if (!string.IsNullOrEmpty(Endpoint))
                {
                    requestUri = new Uri(ClusterUri,new Uri(Endpoint,UriKind.Relative));
                }
                var request = WebRequest.Create(requestUri);
                request.Method = Method;
                if (!string.IsNullOrEmpty(RequestBody))
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(RequestBody);
                    request.ContentLength = byteArray.Length;
                    Stream dataStream = request.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                WebResponse response = request.GetResponse();
                var reader = new StreamReader(response.GetResponseStream());
                Thread.Sleep(3000);

                Response = TryFormatIfJson(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                Response = ex.ToString();
            }
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
                return JsonConvert.SerializeObject(obj,Formatting.Indented);
            }
            catch
            {
                return response;
            }

        }

        private Uri ClusterUri { get; set; }

        public void Handle(ClusterUriChanged message)
        {
            ClusterUri = message.ClusterUri;
        }

        protected override void OnActivate()
        {
            eventAggregator.Subscribe(this);
            base.OnActivate();
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
            base.OnDeactivate(close);
        }
    }
}
