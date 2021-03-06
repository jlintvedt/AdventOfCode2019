using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day03Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;
        private string input_example3;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "03";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format("R8,U5,L5,D3{0}U7,R6,D4,L4", Environment.NewLine);
            input_example2 = string.Format("R75,D30,R83,U83,L12,D49,R71,U7,L72{0}U62,R66,U55,R34,D71,R55,D58,R83", Environment.NewLine);
            input_example3 = string.Format("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51{0}U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", Environment.NewLine);
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
            var result = AdventOfCode.Day03.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("303", result);
        }

        [TestMethod]
        public void Puzzle1_Example1()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual("6", result);
        }

        [TestMethod]
        public void Puzzle1_Example2()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle1(input_example2);

            // Assert
            Assert.AreEqual("159", result);
        }

        [TestMethod]
        public void Puzzle1_Example3()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle1(input_example3);

            // Assert
            Assert.AreEqual("135", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual("11222", result);
        }

        [TestMethod]
        public void Puzzle2_Example1()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle2(input_example1);

            // Assert
            Assert.AreEqual("30", result);
        }

        [TestMethod]
        public void Puzzle2_Example2()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle2(input_example2);

            // Assert
            Assert.AreEqual("610", result);
        }

        [TestMethod]
        public void Puzzle2_Example3()
        {
            // Act
            var result = AdventOfCode.Day03.Puzzle2(input_example3);

            // Assert
            Assert.AreEqual("410", result);
        }
    }
}
