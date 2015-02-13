namespace ElasticOps.Events
{
    public class PreviewValueEvent
    {
        public PreviewValueEvent(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
