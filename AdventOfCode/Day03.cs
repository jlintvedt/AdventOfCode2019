using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/3
    /// </summary>
    public class Day03
    {
        public class WireCrosser
        {
            private Dictionary<(int, int), int> wiremap;
            private string[] wire1, wire2;

            public WireCrosser(string[] firstWire, string[] secondWire)
            {
                wire1 = firstWire;
                wire2 = secondWire;

                wiremap = new Dictionary<(int, int), int>();
            }

            public void MapWire(string[] wire)
            {
                var pos = (0, 0);
                int steps = 0;

                foreach (var stretch in wire)
                {
                    ParseStretch(stretch, out Direction dir, out int length);

                    switch (dir)
                    {
                        case Direction.Left:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1 - 1, pos.Item2);
                                steps++;
                                wiremap.TryAdd(pos, steps);
                            }
                            break;
                        case Direction.Right:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1 + 1, pos.Item2);
                                steps++;
                                wiremap.TryAdd(pos, steps);
                            }
                            break;
                        case Direction.Up:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1, pos.Item2 + 1);
                                steps++;
                                wiremap.TryAdd(pos, steps);
                            }
                            break;
                        case Direction.Down:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1, pos.Item2 -1);
                                steps++;
                                wiremap.TryAdd(pos, steps);
                            }
                            break;
                    }
                }
            }

            public List<((int x, int y) pos, int len1, int len2)> GetWireIntersections(string[] wire)
            {
                var crossings = new List<((int x, int y), int len1, int len2)>();
                var pos = (0, 0);
                int steps = 0;

                foreach (var stretch in wire)
                {
                    ParseStretch(stretch, out Direction dir, out int length);

                    switch (dir)
                    {
                        case Direction.Left:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1 - 1, pos.Item2);
                                steps++;
                                if (wiremap.ContainsKey(pos))
                                {
                                    crossings.Add((pos, wiremap[pos], steps));
                                }
                            }
                            break;
                        case Direction.Right:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1 + 1, pos.Item2);
                                steps++;
                                if (wiremap.ContainsKey(pos))
                                {
                                    crossings.Add((pos, wiremap[pos], steps));
                                }
                            }
                            break;
                        case Direction.Up:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1, pos.Item2 + 1);
                                steps++;
                                if (wiremap.ContainsKey(pos))
                                {
                                    crossings.Add((pos, wiremap[pos], steps));
                                }
                            }
                            break;
                        case Direction.Down:
                            for (int i = 0; i < length; i++)
                            {
                                pos = (pos.Item1, pos.Item2 - 1);
                                steps++;
                                if (wiremap.ContainsKey(pos))
                                {
                                    crossings.Add((pos, wiremap[pos], steps));
                                }
                            }
                            break;
                    }
                }

                return crossings;
            }

            public int FindDistanceToClosestIntersection()
            {
                MapWire(wire1);
                var intersections = GetWireIntersections(wire2);

                var shortest = Int32.MaxValue;
                foreach (var (pos, _, _) in intersections)
                {
                    var dist = CalculateManhattanDistance(pos.x, pos.y);
                    if (dist < shortest)
                    {
                        shortest = dist;
                    }
                }

                return shortest;
            }

            public int FindFewestStepsToIntersection()
            {
                MapWire(wire1);
                var intersections = GetWireIntersections(wire2);

                var shortest = Int32.MaxValue;
                foreach (var (_, len1, len2) in intersections)
                {
                    var dist = len1 + len2;
                    if (dist < shortest)
                    {
                        shortest = dist;
                    }
                }

                return shortest;
            }

            public int CalculateManhattanDistance(int a, int b)
            {
                a = a < 0 ? -a : a;
                b = b < 0 ? -b : b;
                return a + b;
            }

            private void ParseStretch(string stretch, out Direction direction, out int length)
            {
                var dir = stretch[0];
                switch (dir)
                {
                    case 'L':
                        direction = Direction.Left;
                        break;
                    case 'R':
                        direction = Direction.Right;
                        break;
                    case 'U':
                        direction = Direction.Up;
                        break;
                    case 'D':
                        direction = Direction.Down;
                        break;
                    default:
                        throw new ArgumentException($"Unknown direction [{dir}]");
                }

                length = Convert.ToInt32(stretch.Substring(1));
            }

            private enum Direction
            {
                Left,
                Right,
                Up,
                Down,
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var wires = Common.Common.ParseStringToJaggedStringArray(input, Environment.NewLine, ",");
            var wc = new WireCrosser(wires[0], wires[1]);

            return wc.FindDistanceToClosestIntersection().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            var wires = Common.Common.ParseStringToJaggedStringArray(input, Environment.NewLine, ",");
            var wc = new WireCrosser(wires[0], wires[1]);

            return wc.FindFewestStepsToIntersection().ToString();
        }
    }
}
