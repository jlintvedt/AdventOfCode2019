using System;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/4
    /// </summary>
    public class Day04
    {
        public class PasswordFinder
        {
            private readonly int lowest, highest;
            public int[] current;

            public PasswordFinder(int lowerBound, int upperBound)
            {
                lowest = lowerBound;
                highest = upperBound;

                current = Common.Common.IntToTokenizedArray(lowest);
            }

            public int FindNumberOfPasswordsInRange()
            {
                int numValidPasswords = 0;

                SetLowestNonDecreasing(ref current);

                if (CurentHasAdjecentIdentical())
                {
                    numValidPasswords++;
                }

                while (FindNextValidPassword() <= highest)
                {
                    numValidPasswords++;
                }

                return numValidPasswords;
            }

            public static void SetLowestNonDecreasing(ref int[] array)
            {
                int? replace = null;

                for (int i = 1; i < array.Length; i++)
                {
                    if (replace != null)
                    {
                        array[i] = (int)replace;
                        continue;
                    }
                    if (array[i] < array[i - 1])
                    {
                        replace = array[i - 1];
                        array[i] = (int)replace;
                    }
                }
            }

            public int FindNextValidPassword()
            {
                do
                {
                    IncreaseCurrent();
                } while (!CurentHasAdjecentIdentical());

                return Common.Common.IntArrayToInt(current);
            }

            public void IncreaseCurrent()
            {
                if (current[5] != 9)
                {
                    current[5]++;
                    return;
                }

                for (int pos = current.Length-1; pos >= 0; pos--)
                {
                    if (current[pos] == 9)
                    {
                        continue;
                    }

                    var newValue = current[pos] + 1;
                    for (int i = pos; i < current.Length; i++)
                    {
                        current[i] = newValue;
                    }
                    return;
                }
            }

            public bool CurentHasAdjecentIdentical()
            {
                for (int i = 0; i < current.Length-1; i++)
                {
                    if (current[i] == current[i+1])
                    {
                        return true;
                    }
                }

                return false;
            }

        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var inputs = input.Split("-");
            var low = Convert.ToInt32(inputs[0]);
            var high = Convert.ToInt32(inputs[1]);
            var pf = new PasswordFinder(low, high);
            return pf.FindNumberOfPasswordsInRange().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
