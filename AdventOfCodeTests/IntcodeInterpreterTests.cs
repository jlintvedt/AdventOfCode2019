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
        public void ExecuteInstruction_Add_ImmediateMode()
        {
            // Arrange
            var program = "1001,4,3,4,33";
            var result = "1001,4,3,4,36";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.ExecuteInstruction();

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_Add_ImmediateMode_NegativeNumber()
        {
            // Arrange
            var program = "1101,100,-1,4,0";
            var result = "1101,100,-1,4,99";
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
        public void ExecuteInstruction_Multiply_ImmediateMode()
        {
            // Arrange
            var program = "1002,4,3,4,33";
            var result = "1002,4,3,4,99";
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
        public void ExecuteInstruction_InputThenOutput()
        {
            // Arrange
            var program = "3,0,4,0,99";
            var intcode = new Intcode.Interpreter(program);
            var input = 13;

            // Act & Assert
            intcode.InputBuffer = input;
            intcode.ExecuteInstruction();
            Assert.AreEqual("13,0,4,0,99", intcode.GenerateProgramString());

            intcode.ExecuteInstruction();
            Assert.AreEqual(input, intcode.IoOut);
        }

        [TestMethod]
        public void ExecuteInstruction_Equals_PositionMode()
        {
            // Arrange
            var isequal = "8,5,6,7,99,66,66,-1";
            var expected = "8,5,6,7,99,66,66,1";
            var intcode = new Intcode.Interpreter(isequal);

            // Act && Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());

            // Arrange
            var notequal = "8,5,6,7,99,66,77,-1";
            expected = "8,5,6,7,99,66,77,0";
            intcode = new Intcode.Interpreter(notequal);

            // Act && Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_Equals_ImmediateMode()
        {
            // Arrange
            var isequal = "1108,66,66,5,99,-1";
            var expected = "1108,66,66,5,99,1";
            var intcode = new Intcode.Interpreter(isequal);

            // Act && Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());

            // Arrange
            var notequal = "1108,66,77,5,99,-1";
            expected = "1108,66,77,5,99,0";
            intcode = new Intcode.Interpreter(notequal);

            // Act && Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_LessThan_PositionMode()
        {
            // Arrange
            var islessthan = "7,5,6,7,99,66,77,-1";
            var expected = "7,5,6,7,99,66,77,1";
            var intcode = new Intcode.Interpreter(islessthan);

            // Act && Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());

            // Arrange
            var notlessthan = "7,5,6,7,99,66,66,-1";
            expected = "7,5,6,7,99,66,66,0";
            intcode = new Intcode.Interpreter(notlessthan);

            // Act && Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteInstruction_LessThan_ImmediateMode()
        {
            // Arrange
            var islessthan = "1107,66,77,5,99,-1";
            var expected = "1107,66,77,5,99,1";
            var intcode = new Intcode.Interpreter(islessthan);

            // Act & Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());

            // Arrange
            var notlessthan = "1107,66,66,5,99,-1";
            expected = "1107,66,66,5,99,0";
            intcode = new Intcode.Interpreter(notlessthan);

            // Act & Assert
            intcode.ExecuteInstruction();
            Assert.AreEqual(expected, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteProgram_NoudVerb_AddMultiply()
        {
            // Arrange
            var program = "1,9,10,3,2,3,11,0,99,30,40,50";
            var result = "3500,9,10,70,2,3,11,0,99,30,40,50";
            var intcode = new Intcode.Interpreter(program);

            // Act
            intcode.ExecuteProgram_NounVerb(9, 10);

            // Assert
            Assert.AreEqual(result, intcode.GenerateProgramString());
        }

        [TestMethod]
        public void ExecuteProgram_Equal()
        {
            // Arrange
            // Compares input to 8. Outputs 1 (of it is) or 0( (if it is not).
            var program_pm = "3,9,8,9,10,9,4,9,99,-1,8";
            var program_im = "3,3,1108,-1,8,3,4,3,99";

            // Act & Assert
            // Position Mode
            var intcode = new Intcode.Interpreter(program_pm);
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(4));
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(8));
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(10));

            // Immediate Mode
            intcode = new Intcode.Interpreter(program_im);
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(4));
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(8));
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(10));
        }

        [TestMethod]
        public void ExecuteProgram_LessThan()
        {
            // Arrange
            // Checks if input is less than 8. Outputs 1 (of it is) or 0( (if it is not).
            var program_pm = "3,9,7,9,10,9,4,9,99,-1,8";
            var program_im = "3,3,1107,-1,8,3,4,3,99";

            // Act & Assert
            // Position Mode
            var intcode = new Intcode.Interpreter(program_pm);
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(4));
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(8));
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(10));

            // Immediate Mode
            intcode = new Intcode.Interpreter(program_im);
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(4));
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(8));
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(10));
        }

        [TestMethod]
        public void ExecuteProgram_JumpIf()
        {
            // Arrange
            // Jump tests. Ouputs 0 (if input was 0) or (if input was non-zero)
            var program_pm = "3,12,6,12,15,1,13,14,13,4,13,99,-1,0,1,9";
            var program_im = "3,3,1105,-1,9,1101,0,0,12,4,12,99,1";

            // Act & Assert
            // Position Mode
            var intcode = new Intcode.Interpreter(program_pm);
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(0));
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(1));
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(2));

            // Immediate Mode
            intcode = new Intcode.Interpreter(program_im);
            Assert.AreEqual(0, intcode.ExecuteProgram_InputOutput(0));
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(1));
            Assert.AreEqual(1, intcode.ExecuteProgram_InputOutput(2));
        }

        [TestMethod]
        public void ExecuteProgram_ComparesAndJumps()
        {
            // Arrange
            // Uses multiple instructions on both position and immediate mode.
            // Outputs: 999 (if input <8) or 1000 (if input is 8) or 1001 (if input >8)
            var program = "3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99";
            var intcode = new Intcode.Interpreter(program);

            // Act & Assert
            Assert.AreEqual(999, intcode.ExecuteProgram_InputOutput(7));
            Assert.AreEqual(1000, intcode.ExecuteProgram_InputOutput(8));
            Assert.AreEqual(1001, intcode.ExecuteProgram_InputOutput(9));
        }
    }
}
