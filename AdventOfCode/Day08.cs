using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/8
    /// </summary>
    public class Day08
    {
        public class SpaceImageFormat
        {
            private readonly List<int[,]> Layers;

            public SpaceImageFormat(int[] pixels, int width, int height)
            {
                var pixelPerLayer = width * height;
                if (pixels.Length%pixelPerLayer!=0)
                {
                    throw new ArgumentException($"Number of pixels [{pixels.Length}] must fit in n layers of {width}*{height}={pixelPerLayer}");
                }

                // Parse pixels into layers
                Layers = new List<int[,]>();
                for (int i = 0; i < pixels.Length;)
                {
                    int[,] layer = new int[height,width];
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            layer[y, x] = pixels[i++];
                        }
                    }
                    Layers.Add(layer);
                }
            }

            /// <summary>
            /// FindValidationNumber finds the ValidationNumber for the layer with fewest zeroes.
            /// </summary>
            /// <returns>The ValidationNumber.</returns>
            public int FindValidationNumber()
            {
                int fewestZeroes = int.MaxValue;
                int validationNumber = 0;
                foreach (var layer in Layers)
                {
                    int zeroes = GetLayerValidationNumber(layer, out int val);
                    if (zeroes < fewestZeroes)
                    {
                        fewestZeroes = zeroes;
                        validationNumber = val;
                    }
                }
                return validationNumber;
            }

            /// <summary>
            /// GetLayerValidationNumber calculates validation number and returns number of 0's in a layer.
            /// </summary>
            /// <param name="layer">The layer to calculate for.</param>
            /// <param name="validationNumber">Number of 1's multiplied by number of 2's in layer.</param>
            /// <returns>The number of 0's in layer.</returns>
            private int GetLayerValidationNumber(int[,]layer, out int validationNumber)
            {
                int[] occurrences = new int[10];
                foreach (var p in layer)
                {
                    occurrences[p]++;
                }
                validationNumber = occurrences[1] * occurrences[2];
                return occurrences[0];
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var pixels = Common.Common.ParseStringToIntArray(input);
            var sif = new SpaceImageFormat(pixels, 25, 6);
            return sif.FindValidationNumber().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
