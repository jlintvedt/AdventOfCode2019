using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Intcode
{
    public class Interpreter
    {
        private readonly int[] initialMemory;
        private readonly int[] memory;

        private int instructionPointer;
        private Instruction instruction;

        public int numInstructionsExecuted;

        public int In { get; set; }
        public Queue<int> OutBuffer { get; private set; }

        public Interpreter(string programString)
        {
            initialMemory = programString.Split(new[] { "," }, StringSplitOptions.None).Select(i => Convert.ToInt32(i)).ToArray();
            memory = new int[initialMemory.Length];
            ResetMemory();

            OutBuffer = new Queue<int>();
        }

        public void ResetMemory()
        {
            initialMemory.CopyTo(memory, 0);
            numInstructionsExecuted = 0;
            instructionPointer = 0;
            instruction = GetCurrentInstruction();
        }

        public string GenerateProgramString()
        {
            return string.Join(",", memory);
        }

        public void SetInput(int noun, int verb)
        {
            if (noun < 0 || noun > 99 || verb < 0 || verb > 99)
            {
                throw new ArgumentException($"Noun[{noun}] and verb[{verb}] must be [0,99]");
            }

            SetParamater(1, noun);
            SetParamater(2, verb);
        }

        public void ExecuteProgram(int maxInstructions = 1000)
        {
            while (instruction.Operation != Operation.Halt)
            {
                if (instruction.Operation == Operation.Unknown)
                {
                    throw new Exception($"Unknown instruction [{instruction.Operation}:{memory[instructionPointer]}   pos:{instructionPointer}]");
                }
                else if (numInstructionsExecuted > maxInstructions)
                {
                    throw new Exception($"Max instructions [{maxInstructions}] reached. Aborting");
                }
                ExecuteInstruction();
            }
        }

        public int ExecuteProgram_NounVerb(int noun, int verb, int maxInstructions = 1000)
        {
            SetInput(noun, verb);

            ExecuteProgram(maxInstructions);

            return GetParameter(0);
        }

        public void ExecuteInstruction()
        {
            switch (instruction.Operation)
            {
                case Operation.Add:
                    PerformInstructionAdd();
                    break;
                case Operation.Multiply:
                    PerformInstructionMultiply();
                    break;
                case Operation.Input:
                    PerformInstructionInput();
                    break;
                case Operation.Output:
                    PerformInstructionOutput();
                    break;
                case Operation.JumpIfTrue:
                    PerformInstructionJumpIfTrue();
                    break;
                case Operation.JumpIfFalse:
                    PerformInstructionJumpIfFalse();
                    break;
                case Operation.LessThan:
                    PerformInstructionLessThan();
                    break;
                case Operation.Equals:
                    PerformInstructionEquals();
                    break;
                case Operation.Halt:
                case Operation.Unknown:
                default:
                    throw new Exception($"Cannot execute on instruction [{instruction.Operation}-{memory[instructionPointer]}   pos:{instructionPointer}]");
            }

            numInstructionsExecuted++;
        }

        private void SetParamater(int address, int parameter)
        {
            memory[address] = parameter;
        }

        private int GetParameter(int address)
        {
            return memory[address];
        }

        private void MovePosition(int spaces)
        {
            instructionPointer += spaces;
            instruction = GetCurrentInstruction();
        }

        private void PerformInstructionAdd()
        {
            // Params
            var param1 = memory[instructionPointer + 1];
            var param2 = memory[instructionPointer + 2];
            var param3 = memory[instructionPointer + 3];

            // Values
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];

            // Execute
            memory[param3] = value1 + value2;
            MovePosition(4);
        }

        private void PerformInstructionMultiply()
        {
            // Params
            var param1 = memory[instructionPointer + 1];
            var param2 = memory[instructionPointer + 2];
            var param3 = memory[instructionPointer + 3];

            // Values
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];

            // Execute
            memory[param3] = value1 * value2;
            MovePosition(4);
        }

        private void PerformInstructionInput()
        {
            // Params
            var param1 = memory[instructionPointer + 1];

            // Execute
            memory[param1] = In;
            MovePosition(2);
        }

        private void PerformInstructionOutput()
        {
            // Params
            var param1 = memory[instructionPointer + 1];

            // Values
            var value = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            
            // Execute
            OutBuffer.Enqueue(value);
            MovePosition(2);
        }

        private void PerformInstructionJumpIfTrue()
        {
            throw new NotImplementedException();

            // Params
            var param1 = memory[instructionPointer + 1];
            var param2 = memory[instructionPointer + 2];
            var param3 = memory[instructionPointer + 3];

            // Values
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];

            // Execute
            MovePosition(4);
        }

        private void PerformInstructionJumpIfFalse()
        {
            throw new NotImplementedException();

            // Params
            var param1 = memory[instructionPointer + 1];
            var param2 = memory[instructionPointer + 2];
            var param3 = memory[instructionPointer + 3];

            // Values
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];

            // Execute
            MovePosition(4);
        }

        private void PerformInstructionLessThan()
        {
            throw new NotImplementedException();

            // Params
            var param1 = memory[instructionPointer + 1];
            var param2 = memory[instructionPointer + 2];
            var param3 = memory[instructionPointer + 3];

            // Values
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];

            // Execute
            MovePosition(4);
        }

        private void PerformInstructionEquals()
        {
            throw new NotImplementedException();

            // Params
            var param1 = memory[instructionPointer + 1];
            var param2 = memory[instructionPointer + 2];
            var param3 = memory[instructionPointer + 3];

            // Values
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];

            // Execute
            MovePosition(4);
        }

        private Instruction GetCurrentInstruction()
        {
            return InstructionParser.GetInstruction(memory[instructionPointer]);
        }
    }

    public class Instruction
    {
        public readonly Operation Operation;
        public readonly Mode[] Mode;

        public Instruction(Operation operation, Mode[] modes)
        {
            Operation = operation;
            Mode = modes;
        }
    }

    public static class InstructionParser
    {
        public static readonly Dictionary<int, Instruction> KnownInstructions = new Dictionary<int, Instruction>
        {
            // Add [1]
            { 0001, new Instruction(Operation.Add, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0101, new Instruction(Operation.Add, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1001, new Instruction(Operation.Add, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1101, new Instruction(Operation.Add, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Multiply [2]
            { 0002, new Instruction(Operation.Multiply, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0102, new Instruction(Operation.Multiply, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1002, new Instruction(Operation.Multiply, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1102, new Instruction(Operation.Multiply, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Input [3]
            { 3, new Instruction(Operation.Input, null) },
            //Output [4]
            { 004, new Instruction(Operation.Output, new Mode[1]{Mode.Position}) },
            { 104, new Instruction(Operation.Output, new Mode[1]{Mode.Immediate}) },
            // Jump-if-true [5]
            { 0005, new Instruction(Operation.JumpIfTrue, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0105, new Instruction(Operation.JumpIfTrue, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1005, new Instruction(Operation.JumpIfTrue, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1105, new Instruction(Operation.JumpIfTrue, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Jump-if-false [6]
            { 0006, new Instruction(Operation.JumpIfFalse, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0106, new Instruction(Operation.JumpIfFalse, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1006, new Instruction(Operation.JumpIfFalse, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1106, new Instruction(Operation.JumpIfFalse, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Less than [7]
            { 0007, new Instruction(Operation.LessThan, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0107, new Instruction(Operation.LessThan, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1007, new Instruction(Operation.LessThan, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1107, new Instruction(Operation.LessThan, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Equals [8]
            { 0008, new Instruction(Operation.Equals, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0108, new Instruction(Operation.Equals, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1008, new Instruction(Operation.Equals, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1108, new Instruction(Operation.Equals, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Halt
            { 99, new Instruction(Operation.Halt, null) },
        };

        public static readonly Dictionary<int, Mode[]> KnownModes = new Dictionary<int, Mode[]>
        {
            { 1, new Mode[1] { Mode.Immediate } },
            { 10, new Mode[2] { Mode.Position, Mode.Immediate } },
            { 11, new Mode[2] { Mode.Immediate, Mode.Immediate } },
        };

        public static Instruction GetInstruction(int rawInstruction)
        {
            if (KnownInstructions.TryGetValue(rawInstruction, out Instruction inst))
            {
                return inst;
            }
            return ParseInstruction(rawInstruction);
        }

        public static Instruction ParseInstruction(int rawInstruction)
        {
            int opcode = rawInstruction % 100;
            int modesraw = (rawInstruction - opcode) / 100;

            switch (opcode)
            {
                case 1:
                    return new Instruction(Operation.Add, GetModes(modesraw, 2));
                case 2:
                    return new Instruction(Operation.Multiply, GetModes(modesraw, 2));
                case 3:
                    return new Instruction(Operation.Input, null);
                case 4:
                    return new Instruction(Operation.Output, GetModes(modesraw, 1));
                case 5:
                    return new Instruction(Operation.JumpIfTrue, GetModes(modesraw, 2));
                case 6:
                    return new Instruction(Operation.JumpIfFalse, GetModes(modesraw, 2));
                case 7:
                    return new Instruction(Operation.LessThan, GetModes(modesraw, 2));
                case 8:
                    return new Instruction(Operation.Equals, GetModes(modesraw, 2));
                case 99:
                    return new Instruction(Operation.Halt, null);
                default:
                    return new Instruction(Operation.Unknown, null);
            }
        }

        public static Mode[] GetModes(int raw, int numModes)
        {
            var modes = new Mode[numModes];
            if (raw == 0)
            {
                return modes;
            }

            if (KnownModes.TryGetValue(raw, out Mode[] m))
            {
                m.CopyTo(modes, 0);
            }
            else
            {
                ParseModes(raw, numModes, ref modes);
            }
            return modes;
        }

        public static void ParseModes(int raw, int numModes, ref Mode[] modes)
        {
            for (int i = 0; i < numModes; i++)
            {
                if (raw % 10 == 0)
                {
                    // Last digit is 0 -> Position mode
                    modes[i] = Mode.Position;
                    raw /= 10;
                }
                else if (raw % 10 == 1)
                {
                    // Last digit is 1 -> Immediate mode
                    modes[i] = Mode.Immediate;
                    raw /= 10;
                }
                else
                {
                    throw new Exception($"Unknown mode in instruction [{raw}] (last digit must be <= 1)");
                }
            }
        }
}

    public enum Operation
    {
        Add,
        Multiply,
        Input,
        Output,
        JumpIfTrue,
        JumpIfFalse,
        LessThan,
        Equals,
        Halt,
        Unknown,
    }

    public enum Mode
    {
        Position,
        Immediate,
    }
}
