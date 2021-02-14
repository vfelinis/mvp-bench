using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mvp.bench
{
    public static partial class MyExtensions
    {
        public static Account ForMax(this List<Account> items)
        {
            var max = items[items.Count - 1];
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Amount > max.Amount)
                {
                    max = items[i];
                }
            }
            return max;
        }

        public static Account LinqMax(this List<Account> items)
        {
            return items.Aggregate((i1, i2) => i1.Amount > i2.Amount ? i1 : i2);
        }
    }
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class TestBenchmark3
    {
        Random _random = new Random();

        [GlobalSetup]
        public void Setup()
        {

        }

        public IEnumerable<List<int>> Data()
        {
            yield return Enumerable.Range(0, 100000).Select((s, i) => _random.Next(0, 100000)).ToList();
            //yield return Enumerable.Range(0, 100000).Select((s, i) => new Account(s.ToString(), _random.Next(0, 100000))).ToList();
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
            return items.AsStructEnumerable().MyMax3(default(IntComparer3));
        }
    }
}
