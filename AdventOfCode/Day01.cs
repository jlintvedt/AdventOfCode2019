using System;
using System.Diagnostics;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/1
    /// </summary>
    public class Day01
    {
        public static int CalculateFuelRequired(int mass)
        {
            return mass / 3 - 2;
        }

        public static int CalculateFuelsFuelRequired(int mass)
        {
            var fuel = CalculateFuelRequired(mass);
            var totalFuel = fuel;

            while (fuel > 8)
            {
                fuel = CalculateFuelRequired(fuel);
                totalFuel += fuel;
            }

            return totalFuel;
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var masses = Common.Common.ParseStringToIntArray(input, delim: Environment.NewLine);
            var totalFuel = 0;

            foreach (var mass in masses)
            {
                totalFuel += CalculateFuelRequired(mass);
            }

            return totalFuel.ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var masses = Common.Common.ParseStringToIntArray(input, delim: Environment.NewLine);
            var totalFuel = 0;

            foreach (var mass in masses)
            {
                totalFuel += CalculateFuelsFuelRequired(mass);
            }

            return totalFuel.ToString();
        }
    }
}
