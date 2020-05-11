using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day01Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;

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
    }
}
