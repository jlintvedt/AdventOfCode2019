using System;
using System.Linq;

namespace AdventOfCode
{
    public class IntcodeInterpreter
    {
        private readonly int[] program;
        private int pos;
        private Instruction currentInstruction;
        public int instructionsExecuted;

        public IntcodeInterpreter(string programString)
        {
            program = programString.Split(new[] { "," }, StringSplitOptions.None).Select(i => Convert.ToInt32(i)).ToArray();
            currentInstruction = GetCurrentInstruction();
        }

        public string GenerateProgramString()
        {
            return string.Join(",", program);
        }

        public void SetValue(int pos, int value)
        {
            program[pos] = value;
        }

        public int GetValue(int pos)
        {
            return program[pos];
        }

        public void ExecuteUntilHalt()
        {
            while (currentInstruction != Instruction.Halt)
            {
                switch (currentInstruction)
                {
                    case Instruction.Add:
                    case Instruction.Multiply:
                        ExecuteInstruction();
                        break;
                    case Instruction.Halt:
                    case Instruction.Unknown:
                    default:
                        throw new Exception($"Invalid instruction in execution-loop [{currentInstruction}-{program[pos]}   pos:{pos}]");
                }
            }
        }

        public void ExecuteInstruction()
        {
            var pos1 = program[pos + 1];
            var pos2 = program[pos + 2];
            var pos3 = program[pos + 3];

            if (currentInstruction == Instruction.Add)
            {
                PerformInstructionAdd(pos1, pos2, pos3);
            }
            else if (currentInstruction == Instruction.Multiply)
            {
                PerformInstructionMultiply(pos1, pos2, pos3);
            }
            else
            {
                throw new Exception($"Cannot execute on instruction [{currentInstruction}-{program[pos]}   pos:{pos}]");
            }
            MovePosition(4);
            instructionsExecuted++;
        }

        private void MovePosition(int spaces)
        {
            pos += spaces;
            currentInstruction = GetCurrentInstruction();
        }


        private void PerformInstructionAdd(int inPos1, int inPos2, int outPos)
        {
            program[outPos] = program[inPos1] + program[inPos2];
        }

        private void PerformInstructionMultiply(int inPos1, int inPos2, int outPos)
        {
            program[outPos] = program[inPos1] * program[inPos2];
        }

        private Instruction GetCurrentInstruction()
        {
            if (program[pos] == 1)
            {
                return Instruction.Add;
            }
            if (program[pos] == 2)
            {
                return Instruction.Multiply;
            }
            if (program[pos] == 99)
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
