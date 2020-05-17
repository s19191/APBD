using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqCwiczenia
{
    public static class ExtensionMethods
    {
        public static IEnumerable<Tuple<T1, T2>> CrossJoin<T1, T2>(this IEnumerable<T1> tableFirst, IEnumerable<T2> tableSecond)
        {
            return tableFirst
                .SelectMany(t1 => tableSecond.Select(t2 => Tuple.Create(t1, t2)));
        }
    }
}
