namespace ElasticOps.Events
{
    public class ErrorOccurredEvent
    {
        public ErrorOccurredEvent(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; set; }
    }
}