using System.Runtime.CompilerServices;

namespace mvp.bench
{
    public interface IFastComparer<T>
    {
        bool IsMore(T left, T right);
        bool IsFastMaxSupport { get; }
        bool FastMax(T left, T right);
    }

    public readonly struct IntFastComparer : IFastComparer<int>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsMore(int left, int right)
        {
            return left > right;
        }

        public bool IsFastMaxSupport => true;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int FastMax(Span<int> span)
        {
            int max = span[0];
            int i = 0;
            int vectorSize = Vector<int>.Count;
            if (span.Length >= (vectorSize * 2))
            {
                var maxVector = new Vector<int>(span.Slice(i, vectorSize));
                for (i = vectorSize; i <= span.Length - vectorSize; i += vectorSize)
                {
                    var current = new Vector<int>(span.Slice(i, vectorSize));
                    if (Vector.GreaterThanAny(current, maxVector))
                    {
                        maxVector = current;
                    }
                }

                Span<int> buffer = stackalloc int[vectorSize];
                maxVector.CopyTo(buffer);
                for (int k = 0; k < buffer.Length; k++)
                {
                    if (buffer[k] > max)
                    {
                        max = buffer[k];
                    }
                }
            }

            for (; i < span.Length; i++)
            {
                if (span[i] > max)
                {
                    max = span[i];
                }
            }
            return max;
        }
    }
}
