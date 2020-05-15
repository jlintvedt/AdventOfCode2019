using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day07Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;
        private string input_example3;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "07";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            input_example2 = "3,23,3,24,1002,24,10,24,1002,23,-1,23,101,5,23,23,1,24,23,23,4,23,99,0,0";
            input_example3 = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
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
            var result = AdventOfCode.Day07.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("880726", result);
        }

        [TestMethod]
        public void Puzzle1_Example1()
        {
            // Act
            var result = AdventOfCode.Day07.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual("43210", result);
        }

        [TestMethod]
        public void Puzzle1_Example2()
        {
            // Act
            var result = AdventOfCode.Day07.Puzzle1(input_example2);

            // Assert
            Assert.AreEqual("54321", result);
        }

        [TestMethod]
        public void Puzzle1_Example3()
        {
            // Act
            var result = AdventOfCode.Day07.Puzzle1(input_example3);

            // Assert
            Assert.AreEqual("65210", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day07.Puzzle2(input_puzzle);

            // Assert
            Assert.AreEqual($"{input_puzzle}_Puzzle2", result);
        }

        [TestMethod]
        public void Puzzle2_Example()
        {
            // Act
            var result = AdventOfCode.Day07.Puzzle2(input_example2);

            // Assert
            Assert.AreEqual($"{input_example2}_Puzzle2", result);
        }
    }
}
