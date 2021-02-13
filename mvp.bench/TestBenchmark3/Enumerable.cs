using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections;

namespace mvp.bench
{
    public readonly ref struct Enumerable<T, TEnumerable, TEnumerator>
        where TEnumerable : struct, IEnumerable<T, TEnumerator>
        where TEnumerator : struct, IEnumerator<T>
    {
        public Enumerable(TEnumerable value) => Value = value;
        public TEnumerable Value { get; }

        public static implicit operator Enumerable<T, TEnumerable, TEnumerator>
            (TEnumerable value) => new Enumerable<T, TEnumerable, TEnumerator>(value);
        public static implicit operator TEnumerable
            (Enumerable<T, TEnumerable, TEnumerator> value) => value.Value;
    }

    public interface IEnumerable<T, TEnumerator> : IEnumerable<T>
        where TEnumerator : struct, IEnumerator<T>
    {
        new TEnumerator GetEnumerator();
    }

    public readonly struct ListEnumerable<T> : IEnumerable<T, ListEnumerator<T>>
    {
        public ListEnumerable(List<T> value) => Value = value;
        public List<T> Value { get; }

        public ListEnumerator<T> GetEnumerator()
            => new ListEnumerator<T>(Value);
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public struct ListEnumerator<T> : IEnumerator<T>
    {
        public ListEnumerator(List<T> list) => (_i, _list, Current) = (-1, list, default);
        readonly List<T> _list;
        int _i;
        public T Current { get; private set; }
        object IEnumerator.Current => Current;

        public void Dispose() { }
        public void Reset() => throw new NotImplementedException();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if ((uint)++_i >= (uint)_list.Count)
                return false;
            Current = _list[_i];
            return true;
        }
    }

    public static class EnumerableExtensions
    {
        public static Enumerable<T, ListEnumerable<T>, ListEnumerator<T>>
            AsStructEnumerable<T>(this List<T> value) => new ListEnumerable<T>(value);

        public static T MyMax3<T, TEnumerable, TEnumerator, TComparer>
            (this Enumerable<T, TEnumerable, TEnumerator> items, TComparer comparer)
            where TEnumerable : struct, IEnumerable<T, TEnumerator>
            where TEnumerator : struct, IEnumerator<T>
            where TComparer : struct, IComparer3<T>
        {
            var enumerable = items.Value;
            var max = enumerable.First();
            foreach (var item in enumerable)
            {
                if (comparer.IsMore(item, max))
                {
                    max = item;
                }
            }
            return max;
        }

        //public static T MyMax3b<T, TEnumerable, TEnumerator, TComparer>
        //    (this Enumerable<T, TEnumerable, TEnumerator> items, TComparer _)
        //    where TEnumerable : struct, IEnumerable<T, TEnumerator>
        //    where TEnumerator : struct, IEnumerator<T>
        //    where TComparer : struct, IComparer3<T>
        //{
        //    var enumerable = items.Value;
        //    Equatable<T, TComparer> max = enumerable.First();
        //    foreach (var item in enumerable)
        //    {
        //        if (item > max)
        //        {
        //            max = item;
        //        }
        //    }
        //    return max;
        //}
    }
}
