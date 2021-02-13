using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mvp.bench
{
    public static partial class MyExtensions
    {
        public static T MyMax2<T, TComparer>
            (this IEnumerable<T> items, TComparer comparer)
            where TComparer : struct, IComparer2<T>
        {
            T max = items.First();
            foreach (var item in items)
            {
                if (comparer.IsMore(item, max))
                {
                    max = item;
                }
            }
            return max;
        }
    }
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class TestBenchmark2
    {
        Random _random = new Random();

        public IEnumerable<List<int>> Data()
        {
            yield return Enumerable.Range(0, 100000).Select((s, i) => _random.Next(0, 100000)).ToList();
        }

        [Benchmark(Baseline = true)]
        [ArgumentsSource(nameof(Data))]
        public int ForMax(List<int> items)
        {
            return items.ForMax();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public int LinqMax(List<int> items)
        {
            return items.LinqMax();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public int MyMax(List<int> items)
        {
            return items.MyMax2(default(IntComparer2));
        }
    }
}
