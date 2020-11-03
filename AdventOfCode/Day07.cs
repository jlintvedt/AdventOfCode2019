using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/7
    /// </summary>
    public class Day07
    {
        static IEnumerable<IEnumerable<long>> phasePermutationsLow = Common.Common.GetPermutations(new List<long>() { 0, 1, 2, 3, 4 }, 5);
        static IEnumerable<IEnumerable<long>> phasePermutationsHigh = Common.Common.GetPermutations(new List<long>() { 5, 6, 7, 8, 9 }, 5);

        public static long ExecuteAmplifierSequence(Intcode.Interpreter intcode, IEnumerable<long> phases)
        {
            long inputOutput = 0;
            foreach (var phase in phases)
            {
                intcode.SetInput(phase);
                inputOutput = intcode.ExecuteProgram_InputOutput(inputOutput);
            }
            return inputOutput;
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var intcode = new Intcode.Interpreter(input);
            long highestSignal = 0;

            foreach (var phaseSettings in phasePermutationsLow)
            {
                var thrusterSignal = ExecuteAmplifierSequence(intcode, phaseSettings);
                if (thrusterSignal > highestSignal)
                {
                    highestSignal = thrusterSignal;
                }
            }

            return highestSignal.ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var outCh = Channel.CreateUnbounded<long>();
            var ampA = new Intcode.Interpreter(input, inputChannel: outCh);
            var ampB = new Intcode.Interpreter(input, inputChannel: ampA.OutputChannel);
            var ampC = new Intcode.Interpreter(input, inputChannel: ampB.OutputChannel);
            var ampD = new Intcode.Interpreter(input, inputChannel: ampC.OutputChannel);
            var ampE = new Intcode.Interpreter(input, inputChannel: ampD.OutputChannel, outputChannel: outCh);
            long highestSignal = 0;

            foreach (var phaseSettings in phasePermutationsHigh)
            {
                // Reset and start all amps with new phase input
                var tasks = new Task[5]
                {
                    ampA.ExecuteProgram_StartAsync(phaseSettings.ElementAt(0)),
                    ampB.ExecuteProgram_StartAsync(phaseSettings.ElementAt(1)),
                    ampC.ExecuteProgram_StartAsync(phaseSettings.ElementAt(2)),
                    ampD.ExecuteProgram_StartAsync(phaseSettings.ElementAt(3)),
                    ampE.ExecuteProgram_StartAsync(phaseSettings.ElementAt(4)),
                };

                // Start feedbackloop
                ampA.SetInput(0);

                //Wait for all tasks to reach halt
                Task.WaitAll(tasks);

                // Check if result is largest
                var result = ampE.GetOutput();
                if (result > highestSignal)
                {
                    highestSignal = result;
                }
            }

            return highestSignal.ToString();
        }
    }
}
