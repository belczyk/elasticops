namespace ElasticOps.Behaviors.Suggesters
{
    public class SuggestItem
    {
        public SuggestItem(string text, SuggestionMode mode)
        {
            Text = text;
            Mode = mode;
        }

        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public SuggestionMode Mode { get; set; }
    }
}