using System;
using System.Linq;

namespace AdventOfCode
{
    public class IntcodeInterpreter
    {
        private readonly int[] initialMemory;
        private readonly int[] memory;
        private Mode[] modes;

        private int instructionPointer;
        private Instruction currentInstruction;

        public int numInstructionsExecuted;

        public IntcodeInterpreter(string programString)
        {
            modes = new Mode[2];
            initialMemory = programString.Split(new[] { "," }, StringSplitOptions.None).Select(i => Convert.ToInt32(i)).ToArray();
            memory = new int[initialMemory.Length];
            ResetMemory();
        }

        public void ResetMemory()
        {
            initialMemory.CopyTo(memory, 0);
            numInstructionsExecuted = 0;
            instructionPointer = 0;
            currentInstruction = GetCurrentInstruction();
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

            while (currentInstruction != Instruction.Halt)
            {
                if (currentInstruction == Instruction.Unknown)
                {
                    throw new Exception($"Unknown instruction [{currentInstruction}:{memory[instructionPointer]}   pos:{instructionPointer}]");
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

            if (currentInstruction == Instruction.Add)
            {
                PerformInstructionAdd(p1, p2, p3);
            }
            else if (currentInstruction == Instruction.Multiply)
            {
                PerformInstructionMultiply(p1, p2, p3);
            }
            else
            {
                throw new Exception($"Cannot execute on instruction [{currentInstruction}-{memory[instructionPointer]}   pos:{instructionPointer}]");
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
            currentInstruction = GetCurrentInstruction();
        }


        private void PerformInstructionAdd(int param1, int param2, int param3)
        {
            var value1 = modes[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = modes[1] == Mode.Immediate ? param2 : memory[param2];
            memory[param3] = value1 + value2;
            MovePosition(4);
        }

        private void PerformInstructionMultiply(int param1, int param2, int param3)
        {
            var value1 = modes[0] == Mode.Immediate ? param1 : memory[param1];
            var value2 = modes[1] == Mode.Immediate ? param2 : memory[param2];
            memory[param3] = value1 * value2;
            MovePosition(4);
        }

        private Instruction GetCurrentInstruction()
        {
            return ParseInstruction(memory[instructionPointer], ref modes);
        }

        public static Instruction ParseInstruction(int instruction, ref Mode[] modes)
        {
            int opcode = instruction % 100;
            int modesraw = (instruction - opcode) / 100;

            if (opcode == 1)
            {
                ParseModes(modesraw, 2, ref modes);
                return Instruction.Add;
            }
            if (opcode == 2)
            {
                ParseModes(modesraw, 2, ref modes);
                return Instruction.Multiply;
            }
            if (opcode == 99)
            {
                return Instruction.Halt;
            }
            return Instruction.Unknown;
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

        public enum Instruction
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
}
