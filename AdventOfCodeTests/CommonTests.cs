﻿using AdventOfCode.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCodeTests
{
    [TestClass]
    public class CommonTests
    {
        [TestMethod]
        public void ParseStringToIntArray_NonDelimitedString()
        {
            // Arrange
            var input = "1234321";
            var expected = new int[] { 1, 2, 3, 4, 3, 2, 1 };

            // Act
            var output = Common.ParseStringToIntArray(input);

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        public void ParseStringToIntArray_SpaceDelimitedString()
        {
            // Arrange
            var input = "1 2 3 4 3 2 1";
            var expected = new int[] { 1, 2, 3, 4, 3, 2, 1 };

            // Act
            var output = Common.ParseStringToIntArray(input, " ");

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        public void ParseStringToIntArray_NewlineDelimitedString()
        {
            // Arrange
            var input = "1 2 3 4 3 2 1".Replace(" ", Environment.NewLine);
            var expected = new int[] { 1, 2, 3, 4, 3, 2, 1 };

            // Act
            var output = Common.ParseStringToIntArray(input, Environment.NewLine);

            // Assert
            CollectionAssert.AreEqual(expected, output);
        }

        [TestMethod]
        public void ParseStringToJaggedIntArray_CommaAndSpaceDelim()
        {
            // Arrange
            var input = "1 2 3,4 5 6,7 8 9";
            var expected = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 7, 8, 9 }
            };

            // Act
            var output = Common.ParseStringToJaggedIntArray(input, ",", " ");

            // Assert
            for (int i = 0; i < expected.Length; i++)
            {
                CollectionAssert.AreEqual(expected[i], output[i]);
            }
        }

        [TestMethod]
        public void ParseStringToJaggedIntArray_NewlineAndTabDelim()
        {
            // Arrange
            var input = string.Format("1{1}2{1}3{0}4{1}5{1}6{0}7{1}8{1}9", Environment.NewLine, "\t");
            var expected = new int[][]
            {
                new int[] { 1, 2, 3 },
                new int[] { 4, 5, 6 },
                new int[] { 7, 8, 9 }
            };

            // Act
            var output = Common.ParseStringToJaggedIntArray(input, Environment.NewLine, "\t");

            // Assert
            for (int i = 0; i < expected.Length; i++)
            {
                CollectionAssert.AreEqual(expected[i], output[i]);
            }
        }

        [TestMethod]
        public void ParseStringToJaggedStringArray_NewlineAndCommaDelim()
        {
            // Arrange
            var input = string.Format("1{1}2{1}3{0}4{1}5{1}6{0}7{1}8{1}9", Environment.NewLine, ",");
            var expected = new string[][]
            {
                new string[] { "1", "2", "3" },
                new string[] { "4", "5", "6" },
                new string[] { "7", "8", "9" }
            };

            // Act
            var output = Common.ParseStringToJaggedStringArray(input, Environment.NewLine, ",");

            // Assert
            for (int i = 0; i < expected.Length; i++)
            {
                CollectionAssert.AreEqual(expected[i], output[i]);
            }
        }

        [TestMethod]
        public void ConvertHexStringToBitArray_ValidInput()
        {
            // Arrange
            var expected_a = new int[] { 1, 0, 1, 0 };
            var expected_abc = new int[] { 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 0 };
            var expected_09af = new int[] { 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 0, 1, 1, 1, 1 };

            // Act & Assert
            CollectionAssert.AreEqual(expected_a, Common.ConvertHexStringToBitArray("a"));
            CollectionAssert.AreEqual(expected_abc, Common.ConvertHexStringToBitArray("abc"));
            CollectionAssert.AreEqual(expected_09af, Common.ConvertHexStringToBitArray("09af"));
        }

        [TestMethod]
        [ExpectedException (typeof(ArgumentException))]
        public void ConvertHexStringToBitArray_InvalidInput()
        {
            // Act
            Common.ConvertHexStringToBitArray("x");
        }

    }
}
