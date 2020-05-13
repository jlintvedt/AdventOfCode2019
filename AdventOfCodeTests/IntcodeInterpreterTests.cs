using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using AdventOfCode;
using Intcode = AdventOfCode.Intcode;
using Mode = AdventOfCode.Intcode.Mode;
using Operation = AdventOfCode.Intcode.Operation;
using Instruction = AdventOfCode.Intcode.Instruction;


namespace AdventOfCodeTests
{
    [TestClass]
    public class IntcodeInterpreterTests
    {
        [TestMethod]
        public void Constructor_ParseInput()
        {
            // Arrange
            var program = "1,0,0,0,99";

            // Act
            var intcode = new Intcode.Interpreter(program);

            // Assert
            Assert.AreEqual(program, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void SetInput_ShouldUpdateProgram()
        {
            // Arrange
            var program = "1,0,0,0,99";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.SetInput(22, 33);

            // Assert
            Assert.AreEqual("1,22,33,0,99", intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_Add()
        {
            // Arrange
            var program = "1,0,0,0,99";
            var result = "2,0,0,0,99";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.ExecuteInstruction();

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_Multiply()
        {
            // Arrange
            var program = "2,3,0,3,99";
            var result = "2,3,0,6,99";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.ExecuteInstruction();

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_Multiply2()
        {
            // Arrange
            var program = "2,4,4,5,99,0";
            var result = "2,4,4,5,99,9801";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.ExecuteInstruction();

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_AddThenMultiply()
        {
            // Arrange
            var program = "1,1,1,4,99,5,6,0,99";
            var intcode = new Intcode.Interpreter(program);

            // Act & Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual("1,1,1,4,2,5,6,0,99", intcode.GenerateProgramString());

            intcode.ExecuteInstruction();
            Assert.AreEqual("30,1,1,4,2,5,6,0,99", intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteProgram_AddMultiply()
        {
            // Arrange
            var program = "1,9,10,3,2,3,11,0,99,30,40,50";
            var result = "3500,9,10,70,2,3,11,0,99,30,40,50";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.ExecuteProgram(9, 10);

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }
    }
}
