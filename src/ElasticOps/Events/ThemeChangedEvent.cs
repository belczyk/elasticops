namespace ElasticOps.Events
{
    public class ThemeChangedEvent
    {
        public ThemeChangedEvent(string theme, bool isDark)
        {
            Theme = theme;
            IsDark = isDark;
        }

        public string Theme { get; set; }

        public bool IsDark { get; set; }
    }
}