using System;


namespace ElasticOps
{
    public static class Ensure
    {
        public static void ArgumentNotNull(object value, string valueName)
        {
            if(value ==null)
                throw  new ArgumentNullException(valueName);
        }
    }
}
