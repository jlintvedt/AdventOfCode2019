using AdventOfCode;
using BenchmarkDotNet.Attributes;

namespace AdventOfCodeBenchmark
{
    public class Day00Benchmark
    {
        string input;

        [Params(1000000)]
        public int N;

        [GlobalSetup]
        public void Setup()
        {
            input = AdventOfCodeTests.Resources.Input.D00_Puzzle;
        }

        [Benchmark]
        public string D01_P1() => Day00.Puzzle1(input);

        [Benchmark]
        public string D01_P2() => Day00.Puzzle2(input);
    }
}
