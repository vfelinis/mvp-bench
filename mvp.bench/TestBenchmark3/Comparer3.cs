using System.Runtime.CompilerServices;

namespace mvp.bench
{
    public interface IComparer3<T>
    {
        bool IsMore(T left, T right);
    }

    public readonly struct IntComparer3 : IComparer3<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(int left, int right)
        {
            return left > right;
        }
    }

    public readonly struct AccountComparer3 : IComparer3<Account>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(Account left, Account right)
        {
            return left.Amount > right.Amount;
        }
    }
}
