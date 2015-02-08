namespace ElasticOps.Extensions
{
    public static class IntExtenstions
    {

        public static string Humanize(this int number)
        {
            return ((long) number).Humanize();
        }

    }
}
