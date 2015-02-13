using Serilog.Events;

namespace ElasticOps.Events
{
    public class LogEntryCreatedEvent
    {
        public LogEntryCreatedEvent(LogEvent logEvent)
        {
            LogEvent = logEvent;
        }

        public LogEvent LogEvent { get; set; }
    }
}