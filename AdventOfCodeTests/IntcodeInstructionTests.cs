using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

using AdventOfCode;
using Intcode = AdventOfCode.Intcode;
using Mode = AdventOfCode.Intcode.Mode;
using Operation = AdventOfCode.Intcode.Operation;
using Instruction = AdventOfCode.Intcode.Instruction;
using InstructionParser = AdventOfCode.Intcode.InstructionParser;


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
            var instruction = InstructionParser.ParseInstruction(addPositionPosition);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Position }, instruction.Mode);

            instruction = InstructionParser.ParseInstruction(addImmediatePosition);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Position }, instruction.Mode);

            instruction = InstructionParser.ParseInstruction(addPositionImmediate);
            Assert.AreEqual(Operation.Add, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Immediate }, instruction.Mode);

            instruction = InstructionParser.ParseInstruction(addImmediateImmediate);
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
            var instruction = InstructionParser.ParseInstruction(multiplyPositionPosition);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Position }, instruction.Mode);

            instruction = InstructionParser.ParseInstruction(multiplyImmediatePosition);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Position }, instruction.Mode);

            instruction = InstructionParser.ParseInstruction(multiplyPositionImmediate);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Position, Mode.Immediate }, instruction.Mode);

            instruction = InstructionParser.ParseInstruction(multiplyImmediateImmediate);
            Assert.AreEqual(Operation.Multiply, instruction.Operation);
            CollectionAssert.AreEqual(new Mode[2] { Mode.Immediate, Mode.Immediate }, instruction.Mode);
        }

        [TestMethod]
        public void GetInstruction_ShouldMatchParseInstruction()
        {
            foreach (var inst in InstructionParser.KnownInstructions.Keys)
            {
                var known = InstructionParser.GetInstruction(inst);
                var parsed = InstructionParser.ParseInstruction(inst);
                Assert.AreEqual(known.Operation, parsed.Operation);
                CollectionAssert.AreEqual(known.Mode, parsed.Mode, $"Missmatch for instruction [{inst}]");
            }
        }
    }
}
