using System;
using System.Collections.ObjectModel;
using Caliburn.Micro;
using ElasticOps.Attributes;
using ElasticOps.Com;

namespace ElasticOps.ViewModels.ManagmentScreens
{
    public class LogEventViewModel
    {
        public string Text { get; set; }
        public string Level { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }

    [Priority(100)]
    public class ConsoleViewModel : Screen, IManagmentScreen, IHandle<LogEntryCreatedEvent>
    {
        public ConsoleViewModel(Infrastructure infrastructure)
        {
            DisplayName = "Logs";
            LogEntries = new ObservableCollection<LogEventViewModel>();
            infrastructure.EventAggregator.Subscribe(this);
        }

        public ObservableCollection<LogEventViewModel> LogEntries { get; set; } 

        public void Handle(LogEntryCreatedEvent @event)
        {
            LogEntries.Add(new LogEventViewModel
            {
                Level = @event.LogEvent.Level.ToString(),
                Text = @event.LogEvent.RenderMessage(),
                Timestamp = @event.LogEvent.Timestamp,
            });
        }

    }
}
