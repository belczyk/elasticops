namespace ElasticOps.Events
{
    public class AccentChangedEvent
    {
        public AccentChangedEvent(string accent)
        {
            Accent = accent;
        }

        public string Accent { get; set; }
    }
}