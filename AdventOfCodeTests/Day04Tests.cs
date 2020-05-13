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
            Assert.AreEqual("2150", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day04.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"{input_puzzle}_Puzzle2", result);
        }

        // == == == == == PasswordFinder == == == == ==
        [TestMethod]
        public void PasswordFinder_SetLowestNonDecreasing_ShouldAdjust()
        {
            // Arrange
            var input = new int[6] { 1, 2, 3, 0, 1, 2 };
            var expected = new int[6] { 1, 2, 3, 3, 3, 3 };

            // Act
            AdventOfCode.Day04.PasswordFinder.SetLowestNonDecreasing(ref input);

            // Assert
            CollectionAssert.AreEqual(expected, input);
        }

        [TestMethod]
        public void PasswordFinder_SetLowestNonDecreasing_NoAdjustmentNeeded()
        {
            // Arrange
            var input = new int[6] { 1, 2, 3, 4, 5, 5 };
            var expected = new int[6] { 1, 2, 3, 4, 5, 5 };

            // Act
            AdventOfCode.Day04.PasswordFinder.SetLowestNonDecreasing(ref input);

            // Assert
            CollectionAssert.AreEqual(expected, input);
        }
    }
}
