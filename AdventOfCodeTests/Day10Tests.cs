using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day10Tests
    {
        private string input_puzzle;
        private string input_example1;
        private string input_example2;
        private string input_example3;
        private string input_example4;
        private string input_example5;

        [TestInitialize]
        public void LoadInput()
        {
            string day = "10";
            input_puzzle = Resources.Input.ResourceManager.GetObject($"D{day}_Puzzle").ToString();
            input_example1 = string.Format(Resources.Input.ResourceManager.GetObject($"D{day}_E1").ToString(), Environment.NewLine);
            input_example2 = string.Format(Resources.Input.ResourceManager.GetObject($"D{day}_E2").ToString(), Environment.NewLine);
            input_example3 = string.Format(Resources.Input.ResourceManager.GetObject($"D{day}_E3").ToString(), Environment.NewLine);
            input_example4 = string.Format(Resources.Input.ResourceManager.GetObject($"D{day}_E4").ToString(), Environment.NewLine);
            input_example5 = string.Format(Resources.Input.ResourceManager.GetObject($"D{day}_E5").ToString(), Environment.NewLine);
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
            var result = AdventOfCode.Day10.Puzzle1(input_puzzle);

            // Assert
            Assert.AreEqual("319", result);
        }

        [TestMethod]
        public void Puzzle1_Example1()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle1(input_example1);

            // Assert
            Assert.AreEqual("8", result);
        }

        [TestMethod]
        public void Puzzle1_Example2()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle1(input_example2);

            // Assert
            Assert.AreEqual("33", result);
        }

        [TestMethod]
        public void Puzzle1_Example3()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle1(input_example3);

            // Assert
            Assert.AreEqual("35", result);
        }

        [TestMethod]
        public void Puzzle1_Example4()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle1(input_example4);

            // Assert
            Assert.AreEqual("41", result);
        }

        [TestMethod]
        public void Puzzle1_Example5()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle1(input_example5);

            // Assert
            Assert.AreEqual("210", result);
        }

        [TestMethod]
        public void Puzzle2()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle2(input_puzzle, (31,20));

            // Assert
            Assert.AreEqual("517", result);
        }

        [TestMethod]
        public void Puzzle2_Example()
        {
            // Act
            var result = AdventOfCode.Day10.Puzzle2(input_example5, (11,13));

            // Assert
            Assert.AreEqual("802", result);
        }

        [TestMethod]
        public void MonitoringStation_Example1_Validation()
        {
            // Arrange
            var ms = new AdventOfCode.Day10.MonitoringStation(input_example1);

            // Act & Assert
            Assert.AreEqual(7, ms.NumDetectableAsteroids((1, 0)));
            Assert.AreEqual(7, ms.NumDetectableAsteroids((4, 0)));
            Assert.AreEqual(6, ms.NumDetectableAsteroids((0, 2)));
            Assert.AreEqual(7, ms.NumDetectableAsteroids((1, 2)));
            Assert.AreEqual(7, ms.NumDetectableAsteroids((2, 2)));
            Assert.AreEqual(7, ms.NumDetectableAsteroids((3, 2)));
            Assert.AreEqual(5, ms.NumDetectableAsteroids((4, 2)));
            Assert.AreEqual(7, ms.NumDetectableAsteroids((4, 3)));
            Assert.AreEqual(8, ms.NumDetectableAsteroids((3, 4)));
            Assert.AreEqual(7, ms.NumDetectableAsteroids((4, 4)));
        }

        [TestMethod]
        public void CalculateDiastanceAndRadian_AlligedPoints_HasSameRadian()
        {
            // Arrange
            var origin = (5, 5);

            var allignedPoints = new List<(int, int)>()
            {
                (5, 4),(5, 3), // Up
                (6, 3),(7, 1), // Up Up Right
                (6, 4),(7, 3), // Up right
                (7, 4),(9, 3), // Up Right Right
                (6, 5),(7, 5), // Right
                (6, 6),(7, 7), // Down Right
                (5, 6),(5, 7), // Down
                (4, 6),(3, 7), // Down Left
                (4, 5),(3, 5), // Left
                (4, 4),(3, 3), // Up Left
            };

            for (int i = 0; i < allignedPoints.Count/2; i++)
            {
                var (len, rad) = AdventOfCode.Day10.MonitoringStation.CalculateDiastanceAndRadian(origin, allignedPoints[i*2]);
                var (len2, rad2) = AdventOfCode.Day10.MonitoringStation.CalculateDiastanceAndRadian(origin, allignedPoints[i*2+1]);
                Assert.AreEqual(rad, rad2);
            }

        }

        [TestMethod]
        public void CalculateDiastanceAndRadian_AroundTheClock()
        {
            // Arrange
            var origin = (5, 5);
            var prevRad = -1.0;

            var roundTheClock = new List<(int, int)>() {
                (5,1), (6,2), (7,3), (8,4),
                (9,5), (8,6), (7,7), (6,8),
                (5,9), (4,8), (3,7), (2,6),
                (1,5), (2,4), (3,3), (4,2)
            };

            // Act & Assert
            foreach (var point in roundTheClock)
            {
                var (len, radian) = AdventOfCode.Day10.MonitoringStation.CalculateDiastanceAndRadian(origin, point);
                Assert.IsTrue(prevRad < radian);
                prevRad = radian;
            }
        }
    }
}
