namespace ElasticOps.Extensions
{
    public static class IntExtensions
    {
        public static string Humanize(this int number)
        {
            return ((long) number).Humanize();
        }
    }
}