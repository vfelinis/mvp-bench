using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace mvp.bench
{
    public static class SpanExtensions
    {
        public static Span<T> AsSpan<T>(this List<T> value) => CollectionsMarshal.AsSpan(value);

        public static T MyMax5<T, TComparer>
            (this Span<T> span, TComparer comparer)
            where TComparer : struct, IComparer5<T>
        {
            var max = span[span.Length - 1];
            for (int i = span.Length - 1; i >= 0; i--)
            {
                if (comparer.IsMore(span[i], max))
                {
                    max = span[i];
                }
            }
            return max;
        }




        public static T MyFastMax<T, TComparer>
            (this Span<T> span, TComparer comparer)
            where TComparer : struct, IFastComparer<T>
        {
            if (comparer.IsFastMaxSupport)
            {
                return comparer.FastMax(span);
            }
            else
            {
                var max = span[span.Length - 1];
                for (int i = span.Length - 1; i >= 0; i--)
                {
                    if (comparer.IsMore(span[i], max))
                    {
                        max = span[i];
                    }
                }
                return max;
            }
        }
    }
}
