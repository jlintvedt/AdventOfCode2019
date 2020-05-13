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
            var intcode = new Intcode.Interpreter(input);
            return intcode.ExecuteProgram(12, 2).ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var intcode = new Intcode.Interpreter(input);

            for (int noun = 0; noun < 100; noun++)
            {
                for (int verb = 0; verb < 100; verb++)
                {
                    if (intcode.ExecuteProgram(noun, verb) == 19690720)
                    {
                        return (100 * noun + verb).ToString();
                    }
                    intcode.ResetMemory();
                }
            }

            throw new Exception("Could not find noun and verb that produced 19690720");
        }
    }
}
