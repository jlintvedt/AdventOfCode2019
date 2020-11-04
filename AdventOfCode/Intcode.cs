using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode.Intcode
{
    public class Interpreter
    {
        private readonly long[] initialMemory;
        private readonly long[] memory;

        private long instructionPointer;
        private Instruction instruction;
        private long relativeBase;

        public int numInstructionsExecuted;

        public Channel<long> InputChannel;
        public Channel<long> OutputChannel;

        public Interpreter(string programString, Channel<long> inputChannel = null, Channel<long> outputChannel = null, int? memorySize = null)
        {
            InputChannel = inputChannel ?? Channel.CreateUnbounded<long>();
            OutputChannel = outputChannel ?? Channel.CreateUnbounded<long>();

            if (memorySize == null)
            {
                initialMemory = programString.Split(new[] { "," }, StringSplitOptions.None).Select(i => Convert.ToInt64(i)).ToArray();
            } else
            {
                initialMemory = new long[(long)memorySize];
                var tmp = programString.Split(new[] { "," }, StringSplitOptions.None).Select(i => Convert.ToInt32(i)).ToArray();
                tmp.CopyTo(initialMemory, 0);
            }
            memory = new long[initialMemory.Length];
            ResetMemory();
        }

        public void ResetMemory()
        {
            initialMemory.CopyTo(memory, 0);
            numInstructionsExecuted = 0;
            instructionPointer = 0;
            relativeBase = 0;
            instruction = GetCurrentInstruction();
        }

        public void SetInput(long input)
        {
            if (!InputChannel.Writer.TryWrite(input))
            {
                throw new Exception("Failed to write input to InputChannel");
            }
        }

        public long GetOutput()
        {
            if (!OutputChannel.Reader.TryRead(out long output))
            {
                throw new Exception("Program executed to halt, but provided no output");
            }
            return output;
        }

        public long GetLastOutput()
        {
            var last = Int64.MinValue;
            while (OutputChannel.Reader.TryRead(out long output)) { last = output; }
            return last;
        }

        public string GetAllOutput(string delim = ",")
        {

            var sb = new StringBuilder();
            while (OutputChannel.Reader.TryRead(out long output)) { sb.Append(output); sb.Append(","); }
            return sb.ToString();
        }

        public string GenerateProgramString()
        {
            return string.Join(",", memory);
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

        public async Task ExecuteProgramAsync(int maxInstructions = 1000)
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
                await ExecuteInstructionAsync();
            }
        }

        public long ExecuteProgram_NounVerb(int noun, int verb, int maxInstructions = 1000, bool resetMemory = true)
        {
            if (resetMemory)
            {
                ResetMemory();
            }

            if (noun < 0 || noun > 99 || verb < 0 || verb > 99)
            {
                throw new ArgumentException($"Noun[{noun}] and verb[{verb}] must be [0,99]");
            }

            SetParamater(1, noun);
            SetParamater(2, verb);

            ExecuteProgram(maxInstructions);

            return GetParameter(0);
        }

        public long ExecuteProgram_InputOutput(long input, int maxInstructions = 1000, bool resetMemory = true)
        {
            if (resetMemory)
            {
                ResetMemory();
            }

            SetInput(input);

            ExecuteProgram(maxInstructions);

            return GetLastOutput();
        }

        public async Task ExecuteProgram_StartAsync(long input, int maxInstructions = 1000, bool resetMemory = true)
        {
            if (resetMemory)
            {
                ResetMemory();
            }

            SetInput(input);

            await ExecuteProgramAsync(maxInstructions);
        }

        public void ExecuteInstruction()
        {
            switch (instruction.Operation)
            {
                case Operation.Input:
                    AsyncHelper.RunSync(() => PerformInstructionInput());
                    break;
                case Operation.Output:
                    AsyncHelper.RunSync(() => PerformInstructionOutput());
                    break;
                default:
                    ExecuteInstructionInner();
                    break;
            }
            numInstructionsExecuted++;
        }

        public async Task ExecuteInstructionAsync()
        {
            switch (instruction.Operation)
            {
                case Operation.Input:
                    await PerformInstructionInput();
                    break;
                case Operation.Output:
                    await PerformInstructionOutput();
                    break;
                default:
                    await Task.Run(() => ExecuteInstructionInner());
                    break;
            }
            numInstructionsExecuted++;
        }

        /// <summary>
        /// ExecuteInstructionInner executes non-blocking instructions (non-async methods).
        /// </summary>
        private void ExecuteInstructionInner()
        {
            switch (instruction.Operation)
            {
                case Operation.Add:
                    PerformInstructionAdd();
                    break;
                case Operation.Multiply:
                    PerformInstructionMultiply();
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
                case Operation.AdjustRelativeBase:
                    PerformInstructionAdjustRelativeBase();
                    break;
                case Operation.Input:
                case Operation.Output:
                    throw new Exception("These operations has be be handled differently based on async/sync");
                case Operation.Halt:
                case Operation.Unknown:
                default:
                    throw new Exception($"Cannot execute on instruction [{instruction.Operation}-{memory[instructionPointer]}   pos:{instructionPointer}]");
            }
        }

        private void SetParamater(int address, int parameter)
        {
            memory[address] = parameter;
        }

        private long GetParameter(int address)
        {
            return memory[address];
        }

        private void MoveInstructionPointer(int spaces)
        {
            instructionPointer += spaces;
            instruction = GetCurrentInstruction();
        }

        private void SetInstructionPointer(long newPos)
        {
            instructionPointer = newPos;
            instruction = GetCurrentInstruction();
        }

        private void PerformInstructionAdd()
        {
            // Values
            var value1 = GetValue(1);
            var value2 = GetValue(2);

            // Memory position
            var pos = GetMemoryPosition(3);

            // Execute
            memory[pos] = value1 + value2;
            MoveInstructionPointer(4);
        }

        private void PerformInstructionMultiply()
        {
            // Values
            var value1 = GetValue(1);
            var value2 = GetValue(2);

            // Memory position
            var pos = GetMemoryPosition(3);

            // Execute
            memory[pos] = value1 * value2;
            MoveInstructionPointer(4);
        }

        private async Task PerformInstructionInput()
        {
            // Memory position
            var pos = GetMemoryPosition(1);

            // Execute
            memory[pos] = await InputChannel.Reader.ReadAsync();

            MoveInstructionPointer(2);
        }

        private async Task PerformInstructionOutput()
        {
            // Values
            var value = GetValue(1);

            // Execute
            await OutputChannel.Writer.WriteAsync(value);
            MoveInstructionPointer(2);
        }

        private void PerformInstructionJumpIfTrue()
        {
            // Values
            var value1 = GetValue(1);
            var value2 = GetValue(2);

            // Execute
            if (value1 != 0)
            {
                SetInstructionPointer(value2);
            }
            else
            {
                MoveInstructionPointer(3);
            }
        }

        private void PerformInstructionJumpIfFalse()
        {
            // Values
            var value1 = GetValue(1);
            var value2 = GetValue(2);

            // Execute
            if (value1 == 0)
            {
                SetInstructionPointer(value2);
            }
            else
            {
                MoveInstructionPointer(3);
            }
        }

        private void PerformInstructionLessThan()
        {
            // Values
            var value1 = GetValue(1);
            var value2 = GetValue(2);

            // Memory position
            var pos = GetMemoryPosition(3);

            // Execute
            memory[pos] = value1 < value2 ? 1 : 0;
            MoveInstructionPointer(4);
        }

        private void PerformInstructionEquals()
        {
            // Values
            var value1 = GetValue(1);
            var value2 = GetValue(2);

            // Memory position
            var pos = GetMemoryPosition(3);

            // Execute
            memory[pos] = value1 == value2 ? 1 : 0;
            MoveInstructionPointer(4);
        }

        public void PerformInstructionAdjustRelativeBase()
        {
            // Value
            var value = GetValue(1);

            // Execute
            relativeBase += value;
            MoveInstructionPointer(2);
        }

        /// <summary>
        /// GetValue return the value for given parameter position according to the instruction mode
        /// </summary>
        /// <param name="parameterNum">Parameters absolute positon (0 is opcode)</param>
        /// <returns>The value read according to mode.</returns>
        private long GetValue(int parameterNum)
        {
            var param = memory[instructionPointer + parameterNum];
            var value = (instruction.Mode[parameterNum - 1]) switch
            {
                Mode.Position => memory[param],
                Mode.Immediate => param,
                Mode.Relative => memory[relativeBase + param],
                _ => throw new Exception($"Unknown position mode [{instruction.Mode[parameterNum - 1]}"),
            };
            return value;
        }

        private long GetMemoryPosition(int parameterNum)
        {
            var pos = memory[instructionPointer + parameterNum];
            if (instruction.Mode[parameterNum-1] == Mode.Relative)
            {
                pos += relativeBase;
            }
            if (pos >= memory.Length)
            {
                throw new Exception($"Tried to set memory position [{pos}], outside limit of [{memory.Length-1}]");
            }
            return pos;
        }

        private Instruction GetCurrentInstruction()
        {
            return InstructionParser.GetInstruction((int)memory[instructionPointer]);
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

            { 2, new Mode[] { Mode.Relative } },
            { 12, new Mode[] { Mode.Relative, Mode.Immediate } },
            { 21, new Mode[] { Mode.Immediate, Mode.Relative } },
            { 211, new Mode[] { Mode.Immediate, Mode.Immediate, Mode.Relative } },
        };

        public static Instruction GetInstruction(int rawInstruction)
        {
            // Performance optimization: Disabled as it's missing RelativePosition support
            //if (KnownInstructions.TryGetValue(rawInstruction, out Instruction inst))
            //{
            //    return inst;
            //}
            return ParseInstruction(rawInstruction);
        }

        public static Instruction ParseInstruction(int rawInstruction)
        {
            int opcode = rawInstruction % 100;
            int modesraw = (rawInstruction - opcode) / 100;

            switch (opcode)
            {
                case 1:
                    return new Instruction(Operation.Add, GetModes(modesraw, 3));
                case 2:
                    return new Instruction(Operation.Multiply, GetModes(modesraw, 3));
                case 3:
                    return new Instruction(Operation.Input, GetModes(modesraw, 1));
                case 4:
                    return new Instruction(Operation.Output, GetModes(modesraw, 1));
                case 5:
                    return new Instruction(Operation.JumpIfTrue, GetModes(modesraw, 2));
                case 6:
                    return new Instruction(Operation.JumpIfFalse, GetModes(modesraw, 2));
                case 7:
                    return new Instruction(Operation.LessThan, GetModes(modesraw, 3));
                case 8:
                    return new Instruction(Operation.Equals, GetModes(modesraw, 3));
                case 9:
                    return new Instruction(Operation.AdjustRelativeBase, GetModes(modesraw, 1));
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
                else if (raw % 10 == 2)
                {
                    // Last digit is 2 -> Relative mode
                    modes[i] = Mode.Relative;
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
        AdjustRelativeBase,
        Unknown,
    }

    public enum Mode
    {
        Position,
        Immediate,
        Relative,
    }
}
