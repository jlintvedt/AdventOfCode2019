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

        public Interpreter(string programString)
        {
            initialMemory = programString.Split(new[] { "," }, StringSplitOptions.None).Select(i => Convert.ToInt32(i)).ToArray();
            memory = new int[initialMemory.Length];
            ResetMemory();
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

        public int ExecuteProgram(int noun, int verb, int maxInstructions = 1000)
        {
            SetInput(noun, verb);

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

            return GetParameter(0);
        }

        public void ExecuteInstruction()
        {
            var p1 = memory[instructionPointer + 1];
            var p2 = memory[instructionPointer + 2];
            var p3 = memory[instructionPointer + 3];

            if (instruction.Operation == Operation.Add)
            {
                PerformInstructionAdd(p1, p2, p3);
            }
            else if (instruction.Operation == Operation.Multiply)
            {
                PerformInstructionMultiply(p1, p2, p3);
            }
            else
            {
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

        private void PerformInstructionAdd(int param1, int param2, int param3)
        {
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];
            memory[param3] = value1 + value2;
            MovePosition(4);
        }

        private void PerformInstructionMultiply(int param1, int param2, int param3)
        {
            var value1 = instruction.Mode[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = instruction.Mode[1] == Mode.Immediate ? param2 : memory[param2];
            memory[param3] = value1 * value2;
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
        private static readonly Dictionary<int, Instruction> KnownInstructions = new Dictionary<int, Instruction>
        {
            // Add
            { 0001, new Instruction(Operation.Add, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0101, new Instruction(Operation.Add, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1001, new Instruction(Operation.Add, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1101, new Instruction(Operation.Add, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
            // Multiply
            { 0002, new Instruction(Operation.Multiply, new Mode[2]{Mode.Position, Mode.Position } ) },
            { 0102, new Instruction(Operation.Multiply, new Mode[2]{Mode.Immediate, Mode.Position } ) },
            { 1002, new Instruction(Operation.Multiply, new Mode[2]{Mode.Position, Mode.Immediate } ) },
            { 1102, new Instruction(Operation.Multiply, new Mode[2]{Mode.Immediate, Mode.Immediate } ) },
        };

        private static readonly Dictionary<int, Mode[]> KnownModes = new Dictionary<int, Mode[]>
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
            var modes = new Mode[2];

            if (opcode == 1)
            {
                GetModes(modesraw, 2, ref modes);
                return new Instruction(Operation.Add, modes);
            }
            if (opcode == 2)
            {
                GetModes(modesraw, 2, ref modes);
                return new Instruction(Operation.Multiply, modes);
            }
            if (opcode == 99)
            {
                return new Instruction(Operation.Halt, null);
            }
            return new Instruction(Operation.Unknown, null);
        }

        private static void GetModes(int raw, int numModes, ref Mode[] modes)
        {
            if (raw == 0)
            {
                return;
            }

            if (KnownModes.TryGetValue(raw, out Mode[] m))
            {
                m.CopyTo(modes, 0);
            }
            else
            {
                ParseModes(raw, numModes, ref modes);
            }
        }

        private static void ParseModes(int raw, int numModes, ref Mode[] modes)
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
        Halt,
        Unknown,
    }

    public enum Mode
    {
        Position,
        Immediate,
    }
}
