using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using ElasticOps.Attributes;

namespace ElasticOps.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> OrderByPriority<T>(this IEnumerable<T> collection)
        {
            return collection.OrderBy(x => GetPriority(x));
        }

        private static int GetPriority(object obj)
        {
            var attrs = obj.GetType().GetAttributes<PriorityAttribute>(true);
            if (!attrs.Any()) return int.MaxValue;

            return obj.GetType().GetAttributes<PriorityAttribute>(true).Min(x => x.Priority);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable)
            {
                action(t);
            }
        }

    }
}
