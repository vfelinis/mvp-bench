using BenchmarkDotNet.Running;
using System;

namespace mvp.bench
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestBenchmark1>();
            Console.WriteLine(summary);
        }
    }
}
