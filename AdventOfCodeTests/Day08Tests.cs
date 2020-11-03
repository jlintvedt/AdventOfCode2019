using AdventOfCode.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day08Tests
    {
        private string input_puzzle;
        private string input_example1;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "08";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = "123456789012";
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
            var result = AdventOfCode.Day08.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("2975", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day08.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"{input_puzzle}_Puzzle2", result);
        }

        // == == == == == SpaceImageFormat == == == == ==
        [TestMethod]
        public void SpaceImageFormat_FindValidationNumber()
        {
            // Arrange
            var pixels = Common.ParseStringToIntArray(input_example1);
            var sif = new AdventOfCode.Day08.SpaceImageFormat(pixels, 3, 2);

            // Act
            var validationNumber = sif.FindValidationNumber();

            // Assert
            Assert.AreEqual(1, validationNumber);
        }
    }
}
