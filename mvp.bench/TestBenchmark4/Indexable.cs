using System.Collections.Generic;

namespace mvp.bench
{
    public readonly ref struct Indexable<T, TIndexable>
        where TIndexable : struct, IIndexable<T>
    {
        Indexable(TIndexable value) => Value = value;
        public TIndexable Value { get; }

        public static implicit operator Indexable<T, TIndexable>
            (TIndexable value) => new Indexable<T, TIndexable>(value);

        public static implicit operator TIndexable
            (Indexable<T, TIndexable> value) => value.Value;
    }

    public interface IIndexable<T>
    {
        T this[int index] { get; }
        int Length { get; }
    }

    public readonly struct ListIndexable<T> : IIndexable<T>
    {
        public ListIndexable(List<T> value) => Value = value;
        public List<T> Value { get; }

        public T this[int index] => Value[index];

        public int Length => Value.Count;
    }

    public static class IndexableExtensions
    {
        public static Indexable<T, ListIndexable<T>>
            AsIndexable<T>(this List<T> value) => new ListIndexable<T>(value);

        public static T MyMax4<T, TIndexable, TComparer>
            (this Indexable<T, TIndexable> indexable, TComparer comparer)
            where TIndexable : struct, IIndexable<T>
            where TComparer : struct, IComparer4<T>
        {
            var value = indexable.Value;
            var maxValue = value[value.Length - 1];
            for (int i = value.Length - 1; i >= 0; i--)
            {
                if (comparer.IsMore(value[i], maxValue))
                {
                    maxValue = value[i];
                }
            }
            return maxValue;
        }
    }
}
