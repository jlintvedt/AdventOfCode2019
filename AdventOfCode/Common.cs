using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Common
{
    public static class Common
    {
        // == == == == == Parsing == == == == ==
        public static int[] ParseStringToIntArray(string input, string delim = null)
        {
            if (delim == null)
            {
                // No delim, split on each character
                return input.ToCharArray().Select(i => (int)Char.GetNumericValue(i)).ToArray();
            }
            return input.Split(new[] { delim }, StringSplitOptions.None).Select(i => Convert.ToInt32(i)).ToArray();
        }

        public static int[][] ParseStringToJaggedIntArray(string input, string rowDelim = null, string columnDelim = null)
        {
            var rawRows = input.Split(rowDelim);
            var output = new int[rawRows.Length][];

            for (int i = 0; i < rawRows.Length; i++)
            {
                output[i] = ParseStringToIntArray(rawRows[i], columnDelim);
            }

            return output;
        }

        // == == == == == Parsing == == == == ==
        static Dictionary<char, int[]> HexCharacterToBinary = new Dictionary<char, int[]> {
            { '0', new int[]{0,0,0,0} },
            { '1', new int[]{0,0,0,1} },
            { '2', new int[]{0,0,1,0} },
            { '3', new int[]{0,0,1,1} },
            { '4', new int[]{0,1,0,0} },
            { '5', new int[]{0,1,0,1} },
            { '6', new int[]{0,1,1,0} },
            { '7', new int[]{0,1,1,1} },
            { '8', new int[]{1,0,0,0} },
            { '9', new int[]{1,0,0,1} },
            { 'a', new int[]{1,0,1,0} },
            { 'b', new int[]{1,0,1,1} },
            { 'c', new int[]{1,1,0,0} },
            { 'd', new int[]{1,1,0,1} },
            { 'e', new int[]{1,1,1,0} },
            { 'f', new int[]{1,1,1,1} }
        };

        public static int[] ConvertHexStringToBitArray(string hex)
        {
            var hexes = hex.ToCharArray();
            var bitArray = new int[hex.Length*4];
            int[] bits;
            
            for (int i = 0; i < hex.Length; i++)
            {
                if(HexCharacterToBinary.TryGetValue(hexes[i], out bits))
                {
                    Array.Copy(bits, 0, bitArray, i * 4, 4);
                } 
                else
                {
                    throw new ArgumentException($"Invalid hex [{hex}]");
                }
            }

            return bitArray;
        }
    }
}
