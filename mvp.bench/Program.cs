using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;

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
