using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mvp.bench
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class TestBenchmark4
    {
        Random _random = new Random();

        public IEnumerable<List<int>> Data()
        {
            yield return Enumerable.Range(0, 100000).Select((s, i) => _random.Next(0, 100000)).ToList();
            //yield return Enumerable.Range(0, 100000).Select((s, i) => new Account(s.ToString(), _random.Next(0, 100000))).ToArray();
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
            return items.AsIndexable().MyMax4(default(IntComparer4));
        }
    }
}
