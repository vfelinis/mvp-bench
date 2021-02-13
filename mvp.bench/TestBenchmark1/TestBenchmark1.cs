using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mvp.bench
{
    public static partial class MyExtensions
    {
        public static int ForMax(this List<int> items)
        {
            var max = items[items.Count - 1];
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i] > max)
                {
                    max = items[i];
                }
            }
            return max;
        }

        public static int LinqMax(this List<int> items)
        {
            return items.Aggregate((i1, i2) => i1 > i2 ? i1 : i2);
        }

        public static T MyMax1<T>(this IEnumerable<T> items, IComparer1<T> comparer)
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
    public class TestBenchmark1
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
            return items.MyMax1(new IntComparer1());
        }
    }
}
