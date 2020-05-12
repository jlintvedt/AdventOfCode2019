using System;
using System.Linq;

namespace AdventOfCode
{
    public class IntcodeInterpreter
    {
        private readonly int[] initialMemory;
        private readonly int[] memory;

        private int instructionPointer;
        private Instruction currentInstruction;

        public int numInstructionsExecuted;

        public IntcodeInterpreter(string programString)
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
            memory[param3] = memory[param1] + memory[param2];
            MovePosition(4);
        }

        private void PerformInstructionMultiply(int param1, int param2, int param3)
        {
            memory[param3] = memory[param1] * memory[param2];
            MovePosition(4);
        }

        private Instruction GetCurrentInstruction()
        {
            var current = memory[instructionPointer];

            if (current == 1)
            {
                return Instruction.Add;
            }
            if (current == 2)
            {
                return Instruction.Multiply;
            }
            if (current == 99)
            {
                return Instruction.Halt;
            }
            return Instruction.Unknown;
        }

        private enum Instruction
        {
            Add,
            Multiply,
            Halt,
            Unknown
        }
    }
}
