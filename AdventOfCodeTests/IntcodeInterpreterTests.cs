using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using AdventOfCode;
using Mode = AdventOfCode.IntcodeInterpreter.Mode;
using Instruction = AdventOfCode.IntcodeInterpreter.Instruction;
using IntcodeInterpreter = AdventOfCode.IntcodeInterpreter;

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
            var intcode = new IntcodeInterpreter(program);

            // Assert
            Assert.AreEqual(program, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void SetInput_ShouldUpdateProgram()
        {
            // Arrange
            var program = "1,0,0,0,99";
            var intcode = new IntcodeInterpreter(program);

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
            var intcode = new IntcodeInterpreter(program);

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
            var intcode = new IntcodeInterpreter(program);

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
            var intcode = new IntcodeInterpreter(program);

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
            var intcode = new IntcodeInterpreter(program);

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
            var intcode = new IntcodeInterpreter(program);

            // Act
            intcode.ExecuteProgram(9, 10);

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ParseInstruction_Add()
        {
            // Arrange
            var addPositionPosition = 0001;
            var addImmediatePosition = 0101;
            var addPositionImmediate = 1001;
            var addImmediateImmediate = 1101;
            var modes = new Mode[2];

            // Act & Assert
            Assert.AreEqual(Instruction.Add, IntcodeInterpreter.ParseInstruction(addPositionPosition, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Position }, modes);

            Assert.AreEqual(Instruction.Add, IntcodeInterpreter.ParseInstruction(addImmediatePosition, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Position }, modes);

            Assert.AreEqual(Instruction.Add, IntcodeInterpreter.ParseInstruction(addPositionImmediate, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Immediate }, modes);

            Assert.AreEqual(Instruction.Add, IntcodeInterpreter.ParseInstruction(addImmediateImmediate, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Immediate }, modes);
        }

        [TestMethod]
        public void ParseInstruction_Multiply()
        {
            // Arrange
            var multiplyPositionPosition = 0002;
            var multiplyImmediatePosition = 0102;
            var multiplyPositionImmediate = 1002;
            var multiplyImmediateImmediate = 1102;
            var modes = new Mode[2];

            // Act & Assert
            Assert.AreEqual(Instruction.Multiply, IntcodeInterpreter.ParseInstruction(multiplyPositionPosition, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Position }, modes);

            Assert.AreEqual(Instruction.Multiply, IntcodeInterpreter.ParseInstruction(multiplyImmediatePosition, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Position }, modes);

            Assert.AreEqual(Instruction.Multiply, IntcodeInterpreter.ParseInstruction(multiplyPositionImmediate, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Immediate }, modes);

            Assert.AreEqual(Instruction.Multiply, IntcodeInterpreter.ParseInstruction(multiplyImmediateImmediate, ref modes));
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Immediate }, modes);
        }
    }
}
