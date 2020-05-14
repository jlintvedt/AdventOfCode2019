using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    /// <summary>
    /// Template class
    /// https://adventofcode.com/2019/day/6
    /// </summary>
    public class Day06
    {
        public class UniversalOrbitMap
        {
            private Dictionary<string, SpaceObject> objects;
            private SpaceObject centerOfMass;

            public UniversalOrbitMap(string[] orbits)
            {
                objects = new Dictionary<string, SpaceObject>();
                foreach (var orbit in orbits)
                {
                    var obj = orbit.Split(")");
                    var so = GetOrCreateSpaceObject(obj[0]);

                    var inOrbit = GetOrCreateSpaceObject(obj[1]);
                    so.AddObjectToOrbit(inOrbit);
                }
                centerOfMass = objects["COM"];
            }

            public int CalculateTotalNumberOfOrbits()
            {
                return CalculateOrbitCountRecursively(centerOfMass, 0);
            }

            private int CalculateOrbitCountRecursively(SpaceObject so, int distFromCom)
            {
                int count = distFromCom;
                foreach (var obj in so.InOrbit)
                {
                    count += CalculateOrbitCountRecursively(obj, distFromCom+1);
                }
                return count;
            }

            private SpaceObject GetOrCreateSpaceObject(string name)
            {
                SpaceObject so;
                if (objects.TryGetValue(name, out so))
                {
                    return so;
                }
                so = new SpaceObject(name);
                objects.Add(name, so);
                return so;
            }

            private class SpaceObject
            {
                public string Self;
                public List<SpaceObject> InOrbit;

                public SpaceObject(string name)
                {
                    Self = name;
                    InOrbit = new List<SpaceObject>();
                }

                public void AddObjectToOrbit(SpaceObject so)
                {
                    InOrbit.Add(so);
                }
            }
        }

        // == == == == == Puzzle 1 == == == == ==
        public static string Puzzle1(string input)
        {
            var orbits = input.Split(Environment.NewLine);
            var uom = new UniversalOrbitMap(orbits);
            return uom.CalculateTotalNumberOfOrbits().ToString();
        }

        // == == == == == Puzzle 2 == == == == ==
        public static string Puzzle2(string input)
        {
            return input + "_Puzzle2";
        }
    }
}
