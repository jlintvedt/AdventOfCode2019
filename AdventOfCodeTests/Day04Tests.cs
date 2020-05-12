using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day04Tests
    {
        private string input_puzzle;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "04";
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
            var result = AdventOfCode.Day04.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual($"{input_puzzle}_Puzzle1", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day04.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"{input_puzzle}_Puzzle2", result);
        }
    }
}
