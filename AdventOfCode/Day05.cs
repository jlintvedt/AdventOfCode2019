using System;
using System.Linq;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019
    /// </summary>
    public class Day05
    {
        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var intcode = new Intcode.Interpreter(input);
            intcode.In = 1;
            intcode.ExecuteProgram();

            return intcode.OutBuffer.Last().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
