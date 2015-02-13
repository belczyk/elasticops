using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com;
using ElasticOps.Events;

namespace ElasticOps.ViewModels.ManagementScreens
{
    public class LogEventViewModel
    {
        public string Text { get; set; }
        public string Level { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }

    [Priority(100)]
    public class ConsoleViewModel : Screen, IManagementScreen, IHandle<LogEntryCreatedEvent>
    {

        public ConsoleViewModel(Infrastructure infrastructure)
        {
            Ensure.ArgumentNotNull(infrastructure, "infrastructure");

            base.DisplayName = "Logs";
            LogEntries = new ObservableCollection<LogEventViewModel>();
            infrastructure.EventAggregator.Subscribe(this);
        }

        public ObservableCollection<LogEventViewModel> LogEntries { get; private set; }

        public void Handle(LogEntryCreatedEvent message)
        {
            Ensure.ArgumentNotNull(message, "message");

            LogEntries.Add(new LogEventViewModel
            {
                Level = message.LogEvent.Level.ToString(),
                Text = message.LogEvent.RenderMessage(),
                Timestamp = message.LogEvent.Timestamp,
            });
        }
    }
}