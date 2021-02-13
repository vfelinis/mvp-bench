using System.Runtime.CompilerServices;

namespace mvp.bench
{
    public interface IComparer2<T>
    {
        bool IsMore(T left, T right);
    }

    public readonly struct IntComparer2 : IComparer2<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(int left, int right)
        {
            return left > right;
        }
    }
}
