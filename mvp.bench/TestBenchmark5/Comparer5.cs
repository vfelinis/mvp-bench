using System.Runtime.CompilerServices;

namespace mvp.bench
{
    public interface IComparer5<T>
    {
        bool IsMore(T left, T right);
    }

    public readonly struct IntComparer5 : IComparer5<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(int left, int right)
        {
            return left > right;
        }
    }

    public readonly struct AccountComparer5 : IComparer5<Account>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(Account left, Account right)
        {
            return left.Amount > right.Amount;
        }
    }
}
