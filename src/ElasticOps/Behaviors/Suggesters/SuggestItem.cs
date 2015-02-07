
namespace ElasticOps.Behaviors.Suggesters
{
    public class SuggestItem
    {
        public SuggestItem(string text, SugegestionMode mode)
        {
            Text = text;
            Mode = mode;
        }

        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public SugegestionMode Mode { get; set; }
    }
}