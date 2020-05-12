using System;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/2
    /// </summary>
    public class Day02
    {
        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var intcode = new IntcodeInterpreter(input);
            intcode.SetValue(1, 12);
            intcode.SetValue(2, 2);
            intcode.ExecuteUntilHalt();

            return intcode.GetValue(0).ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
