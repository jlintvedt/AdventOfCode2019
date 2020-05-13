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
            //var summary = BenchmarkRunner.Run<Day03Benchmark>();

            var result = Day02.Puzzle2(AdventOfCodeTests.Resources.Input.D02_Puzzle);
        }
    }
}
