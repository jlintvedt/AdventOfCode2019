using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day02Tests
    {
        private string input_puzzle;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "02";
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
            var result = AdventOfCode.Day02.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("3306701", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day02.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual("7621", result);
        }
    }
}
