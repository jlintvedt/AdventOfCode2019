using AdventOfCode;
using BenchmarkDotNet.Attributes;

namespace AdventOfCodeBenchmark
{
    public class Day08Benchmark
    {
        string input;

        [Params(100000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            input = AdventOfCodeTests.Resources.Input.D00_Puzzle;
        }

        [Benchmark]
        public string D00_P1() => Day08.Puzzle1(input);

        [Benchmark]
        public string D00_P2() => Day08.Puzzle2(input);
    }
}
