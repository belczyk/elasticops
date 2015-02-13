namespace ElasticOps.Behaviors.AutoComplete
{
    public class AutoCompleteItem
    {
        public AutoCompleteItem(string label, AutoCompleteMode mode)
        {
            Label = label;
            Mode = mode;
        }

        public string Label { get; set; }

        public AutoCompleteMode Mode { get; set; }

        public override string ToString()
        {
            return Label;
        }

    }
}