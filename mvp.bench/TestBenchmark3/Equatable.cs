using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mvp.bench
{
    public readonly ref struct Equatable<T, TComparer>
        where TComparer : struct, IComparer3<T>
    {
        Equatable(T value) => Value = value;

        public T Value { get; }

        public static bool operator >
            (Equatable<T, TComparer> left, Equatable<T, TComparer> right)
        {
            var comparer = default(TComparer);
            return comparer.IsMore(left, right);
        }

        public static bool operator <
            (Equatable<T, TComparer> left, Equatable<T, TComparer> right)
        {
            var comparer = default(TComparer);
            return comparer.IsMore(right, left);
        }

        public static implicit operator Equatable<T, TComparer>(T value)
            => new Equatable<T, TComparer>(value);

        public static implicit operator T(Equatable<T, TComparer> value) => value.Value;
    }
}
