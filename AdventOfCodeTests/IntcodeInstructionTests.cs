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
    public class IntcodeInstructionTests
    {
        [TestMethod]
        public void ParseInstruction_Add()
        {
            // Arrange
            var addPositionPosition = 0001;
            var addImmediatePosition = 0101;
            var addPositionImmediate = 1001;
            var addImmediateImmediate = 1101;

            // Act & Assert
            var instruction = Intcode.InstructionParser.ParseInstruction(addPositionPosition);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Position }, instruction.Mode);

            instruction = Intcode.InstructionParser.ParseInstruction(addImmediatePosition);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Position }, instruction.Mode);

            instruction = Intcode.InstructionParser.ParseInstruction(addPositionImmediate);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Immediate }, instruction.Mode);

            instruction = Intcode.InstructionParser.ParseInstruction(addImmediateImmediate);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Immediate }, instruction.Mode);
        }

        [TestMethod]
        public void ParseInstruction_Multiply()
        {
            // Arrange
            var multiplyPositionPosition = 0002;
            var multiplyImmediatePosition = 0102;
            var multiplyPositionImmediate = 1002;
            var multiplyImmediateImmediate = 1102;

            // Act & Assert
            var instruction = Intcode.InstructionParser.ParseInstruction(multiplyPositionPosition);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Position }, instruction.Mode);

            instruction = Intcode.InstructionParser.ParseInstruction(multiplyImmediatePosition);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Position }, instruction.Mode);

            instruction = Intcode.InstructionParser.ParseInstruction(multiplyPositionImmediate);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Immediate }, instruction.Mode);

            instruction = Intcode.InstructionParser.ParseInstruction(multiplyImmediateImmediate);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Immediate }, instruction.Mode);
        }
    }
}
