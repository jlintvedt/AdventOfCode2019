using AdventOfCode;
using BenchmarkDotNet.Attributes;

namespace AdventOfCodeBenchmark
{
    public class Day10Benchmark
    {
        string input;

        [Params(1000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            input = AdventOfCodeTests.Resources.Input.D10_Puzzle;
        }

        [Benchmark]
        public string D00_P1() => Day10.Puzzle1(input);

        [Benchmark]
        public string D00_P2() => Day10.Puzzle2(input, (31,20));
    }
}
