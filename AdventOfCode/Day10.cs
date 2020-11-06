using System;
using System.Collections.Generic;
using System.Collections.Immutable;

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

            public (int,int) VaporizeUntilAsteroidX((int,int) station, int asteroidsToVaporize)
            {
                // Map out asteroids based on monitoring station
                var targets = new SortedDictionary<double, SortedList<double, (int,int)>>();
                foreach (var point in asteroids)
                {
                    if (station == point)
                    {
                        continue;
                    }

                    var (dist, rad) = CalculateDiastanceAndRadian(station, point);
                    if (!targets.ContainsKey(rad))
                    {
                        targets[rad] = new SortedList<double, (int, int)>();
                    }
                    targets[rad].Add(dist, (point.x, point.y));
                }

                // Destroy until X
                int detroyed = 0;

                while (true)
                {
                    var radians = targets.Keys.ToImmutableList();
                    foreach (var rad in radians)
                    {
                        var asteroids = targets[rad];
                        // Hit target number X
                        if (asteroidsToVaporize == ++detroyed)
                        {
                            return asteroids.Values[0];
                        }
                        // Keep on vaporizing
                        if (asteroids.Count <= 1)
                        {
                            targets.Remove(rad);
                        } 
                        else
                        {
                            asteroids.RemoveAt(0);
                        }
                    }
                }
            }

            public static (double,double) CalculateDiastanceAndRadian((int x, int y) origin, (int x, int y) point)
            {
                var y = point.x - origin.x;
                var x = -(point.y - origin.y);

                var len = Math.Sqrt(x * x + y * y);
                var rad = Math.Atan2(y, x);

                rad = rad < 0 ? 2 * Math.PI + rad : rad;

                return (len, rad);
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
        public static string Puzzle2(string input, (int,int) monitoringStationCoordinates)
        {
            var ms = new MonitoringStation(input);
            var (x,y) = ms.VaporizeUntilAsteroidX(monitoringStationCoordinates, 200);
            return string.Format("{0}{1:00}", x, y);
        }
    }
}
