using System;
using AdventOfCode;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace AdventOfCodeBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Day01Benchmark>();
        }
    }
}
