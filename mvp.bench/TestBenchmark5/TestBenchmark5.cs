using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mvp.bench
{
    public static partial class MyExtensions
    {
        public static int SpanMax(this List<int> items)
        {
            var span = CollectionsMarshal.AsSpan(items);
            var max = span[span.Length - 1];
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] > max)
                {
                    max = span[i];
                }
            }
            return max;
        }

        public static Account SpanMax(this List<Account> items)
        {
            var span = CollectionsMarshal.AsSpan(items);
            var max = span[span.Length - 1];
            for (int i = 0; i < span.Length; i++)
            {
                if (span[i].Amount > max.Amount)
                {
                    max = span[i];
                }
            }
            return max;
        }
    }

    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class TestBenchmark5
    {
        Random _random = new Random();

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
        public int ForSpanMax(List<int> items)
        {
            return items.SpanMax();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public int LinqMax(List<int> items)
        {
            return items.LinqMax();
        }

        [Benchmark]
        [ArgumentsSource(nameof(Data))]
        public int MyFastMax(List<int> items)
        {
            return items.AsSpan().MyFastMax(default(IntFastComparer));
        }
    }
}
