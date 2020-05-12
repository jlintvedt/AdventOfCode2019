using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day01Tests
    {
        private string input_puzzle;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "01";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
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
            var result = AdventOfCode.Day01.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("3327415", result);
        }

        [TestMethod]
        public void Puzzle1_Examples()
        {
            // Example 1
            var mass = "12";
            var fuel = "2";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle1(mass));

            // Example 2
            mass = "14";
            fuel = "2";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle1(mass));

            // Example 3
            mass = "1969";
            fuel = "654";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle1(mass));

            // Example 4
            mass = "100756";
            fuel = "33583";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle1(mass));
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day01.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual("4988257", result);
        }

        [TestMethod]
        public void Puzzle2_Example1()
        {
            // Example 1
            var mass = "14";
            var fuel = "2";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle2(mass));

            // Example 2
            mass = "1969";
            fuel = "966";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle2(mass));

            // Example 3
            mass = "100756";
            fuel = "50346";
            Assert.AreEqual(fuel, AdventOfCode.Day01.Puzzle2(mass));
        }
    }
}
