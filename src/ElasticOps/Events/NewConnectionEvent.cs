namespace ElasticOps.Events
{
    public class NewConnectionEvent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", MessageId = "0#")]
        public NewConnectionEvent(string url)
        {
            URL = url;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
        public string URL { get; set; }
    }
}