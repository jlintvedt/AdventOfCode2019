using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day06Tests
    {
        private string input_puzzle;
        private string input_example1;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "06";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("COM)B{0}B)C{0}C)D{0}D)E{0}E)F{0}B)G{0}G)H{0}D)I{0}E)J{0}J)K{0}K)L", Environment.NewLine);
        }

        [TestMethod]
        public void Puzzle0_WarmUp()
        {
            // Force performing LoadInput() warm-up as part of this test
        }

        [TestMethod]
        public void Puzzle1()
        {
            // Act
            var result = AdventOfCode.Day06.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("140608", result);
        }

        [TestMethod]
        public void Puzzle1_Example()
        {
            // Act
            var result = AdventOfCode.Day06.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual("42", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day06.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"{input_puzzle}_Puzzle2", result);
        }

        //[TestMethod]
        //public void Puzzle2_Example()
        //{
        //    // Act
        //    var result = AdventOfCode.Day06.Puzzle2(input_example2);

        //    // Assert
        //    Assert.AreEqual($"{input_example2}_Puzzle2", result);
        //}
    }
}
