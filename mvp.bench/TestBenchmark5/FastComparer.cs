using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace mvp.bench
{
    public interface IFastComparer<T>
    {
        bool IsMore(T left, T right);
        bool IsFastMaxSupport { get; }
        T FastMax(Span<T> span);
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
        public unsafe int FastMax(Span<int> span)
        {
            int max = span[0];
            int i = 0;
            int vectorSize = 4;
            if (span.Length >= (vectorSize * 2))
            {
                int* ptr = (int*)Unsafe.AsPointer(ref span[0]);
                var maxVector = Sse41.LoadVector128(ptr + i);
                for (i = vectorSize; i < span.Length - vectorSize; i += vectorSize)
                {
                    var current = Sse41.LoadVector128(ptr + i);
                    maxVector = Sse41.Max(maxVector, current);
                }

                Span<int> buffer = stackalloc int[vectorSize];
                maxVector.AsVector().CopyTo(buffer);
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
