using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/7
    /// </summary>
    public class Day07
    {
        static IEnumerable<IEnumerable<int>> phasePermutations = Common.Common.GetPermutations(new List<int>() { 0, 1, 2, 3, 4 }, 5);

        public static int ExecuteAmplifierSequence(Intcode.Interpreter intcode, IEnumerable<int> phases)
        {
            var inputOutput = 0;
            foreach (var phase in phases)
            {
                intcode.InputBuffer = phase;
                inputOutput = intcode.ExecuteProgram_InputOutput(inputOutput);
            }
            return inputOutput;
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var intcode = new Intcode.Interpreter(input);
            int highestSignal = 0;

            foreach (var phaseSettings in phasePermutations)
            {
                var thrusterSignal = ExecuteAmplifierSequence(intcode, phaseSettings);
                if (thrusterSignal > highestSignal)
                {
                    highestSignal = thrusterSignal;
                }
            }

            return highestSignal.ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
