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
            var idAirConditioner = 1;
            var diagnosticsCode = intcode.ExecuteProgram_InputOutput(idAirConditioner);
            return diagnosticsCode.ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var intcode = new Intcode.Interpreter(input);
            var idThermalRadiator = 5;
            var diagnosticsCode = intcode.ExecuteProgram_InputOutput(idThermalRadiator);
            return diagnosticsCode.ToString();
        }
    }
}
