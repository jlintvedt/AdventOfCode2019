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
                var yRatios = new HashSet<(bool, float)>();

                foreach (var (x, y) in asteroids)
                {
                    int dx = x - asteroid.x;
                    int dy = y - asteroid.y;
                    bool xPositive = dx >= 0;
                    int xAbs = xPositive ? dx : -dx;
                    float yRatio = 0;

                    // edge cases: self or located directly vertical
                    if (dx == 0)
                    {
                        if (dy == 0)
                        {
                            continue;
                        }
                        yRatio = dy < 0 ? float.MinValue : float.MaxValue;
                    }
                    // located at an angle (neither horizontal nor vertical)
                    else if (dy != 0)
                    {
                        yRatio = (float)dy / xAbs;
                    }

                    yRatios.Add((xPositive, yRatio));
                }
                return yRatios.Count;
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
