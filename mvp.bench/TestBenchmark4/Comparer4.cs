using System.Runtime.CompilerServices;

namespace mvp.bench
{
    public interface IComparer4<T>
    {
        bool IsMore(T left, T right);
    }

    public readonly struct IntComparer4 : IComparer4<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(int left, int right)
        {
            return left > right;
        }
    }

    public readonly struct AccountComparer4 : IComparer4<Account>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(Account left, Account right)
        {
            return left.Amount > right.Amount;
        }
    }
}
