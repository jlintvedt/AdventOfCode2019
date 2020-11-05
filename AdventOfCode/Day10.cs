using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/10
    /// </summary>
    public class Day10
    {
        public class MonitoringStation
        {
            public List<(int x,int y)> asteroids;
            private HashSet<(bool, float)> yRatios;

            public MonitoringStation(string mapRaw)
            {
                asteroids = new List<(int x, int y)>();

                // Parse map into asteroid coordinates
                var rows = mapRaw.Split(Environment.NewLine);
                for (int y = 0; y < rows.Length; y++)
                {
                    for (int x = 0; x < rows[y].Length; x++)
                    {
                        if (rows[y][x] == '#')
                        {
                            asteroids.Add((x, y));
                        }
                    }
                }
            }

            public int FindNumDetectableAsteroidsFromBestPosition()
            {
                var mostDetected = 0;

                foreach (var ast in asteroids)
                {
                    var detected = NumDetectableAsteroids(ast);
                    if (detected > mostDetected)
                    {
                        mostDetected = detected;
                    }
                }

                return mostDetected;
            }

            public int NumDetectableAsteroids((int x, int y) asteroid)
            {
                var yRatiosLeft = new HashSet<float>();
                var yRatiosRight = new HashSet<float>();

                int above = 0, below = 0;

                foreach (var other in asteroids)
                {
                    float dx = other.x - asteroid.x;
                    float dy = other.y - asteroid.y;
                    // Check edge for self and edge cases
                    if (dx == 0)
                    {
                        if (dy == 0)
                        {
                            // Self
                            continue;
                        }
                        else if (dy < 0)
                        {
                            above = 1;
                        }
                        else
                        {
                            below = 1;
                        }
                    }
                    else if (dx < 0)
                    {
                        yRatiosLeft.Add(dy / dx);
                    }
                    else
                    {
                        yRatiosRight.Add(dy / dx);
                    }
                }
                return yRatiosLeft.Count + yRatiosRight.Count + above + below;
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var ms = new MonitoringStation(input);
            var mostDetectableAsteroids = ms.FindNumDetectableAsteroidsFromBestPosition();
            return mostDetectableAsteroids.ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
