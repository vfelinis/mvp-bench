using System.Runtime.CompilerServices;

namespace mvp.bench
{
    public interface IComparer1<T>
    {
        bool IsMore(T left, T right);
    }

    public class IntComparer1 : IComparer1<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(int left, int right)
        {
            return left > right;
        }
    }
}
