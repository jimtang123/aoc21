using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day11
    {
        public static string? Run(string whichPart)
        {
            return whichPart switch
            {
                "a" => PartA(),
                "b" => PartB(),
                _   => null
            };
        }

        static IEnumerable<(int, int)> GetSurroundingSquares(int y, int x, int yLen, int xLen)
        {
            foreach (var (nextY, nextX) in new (int, int)[] { (y-1,x-1), (y-1,x), (y-1,x+1), (y,x-1), (y,x+1), (y+1,x-1), (y+1,x), (y+1,x+1) })
            {
                if (nextY < 0
                    || nextY >= yLen
                    || nextX < 0
                    || nextX >= xLen) continue;
                yield return (nextY, nextX);
            }
        }

        // 1640
        static string PartA()
        {
            var f = Utils.Utils.OpenInput("11");
            var octopuses = f
                .Select((line) =>
                    line
                        .Select((c) => c - '0')
                        .ToArray()
                )
                .ToArray();
            int flashes = 0;
            for (int s = 0; s < 100; s++)
            {
                var hasFlashed = new HashSet<(int, int)>();
                // Increase by 1
                octopuses = octopuses
                    .Select((r) => 
                        r
                            .Select((c) => ++c)
                            .ToArray())
                    .ToArray();
                int pastFlashes = flashes;
                do
                {
                    pastFlashes = flashes;
                    for (int y = 0; y < octopuses.Length; y++)
                    {
                        for (int x = 0; x < octopuses[y].Length; x++)
                        {
                            if (octopuses[y][x] > 9 && !hasFlashed.Contains((y, x)))
                            {
                                hasFlashed.Add((y, x));
                                octopuses[y][x] = 0;
                                flashes++;
                                foreach (var (nextY, nextX) in GetSurroundingSquares(y, x, octopuses.Length, octopuses[y].Length))
                                {
                                    if (hasFlashed.Contains((nextY, nextX))) continue;
                                    octopuses[nextY][nextX]++;
                                }
                            }
                        }
                    }
                } while (pastFlashes != flashes);
            }
            return flashes.ToString();
        }

        // 312
        static string PartB()
        {
            var f = Utils.Utils.OpenInput("11");
            var octopuses = f
                .Select((line) =>
                    line
                        .Select((c) => c - '0')
                        .ToArray()
                )
                .ToArray();
            for (int s = 1;; s++)
            {
                var hasFlashed = new HashSet<(int, int)>();
                // Increase by 1
                octopuses = octopuses
                    .Select((r) => 
                        r
                            .Select((c) => ++c)
                            .ToArray())
                    .ToArray();
                int curFlashes = 0;
                do
                {
                    curFlashes = 0;
                    for (int y = 0; y < octopuses.Length; y++)
                    {
                        for (int x = 0; x < octopuses[y].Length; x++)
                        {
                            if (octopuses[y][x] > 9 && !hasFlashed.Contains((y, x)))
                            {
                                hasFlashed.Add((y, x));
                                octopuses[y][x] = 0;
                                curFlashes++;
                                foreach (var (nextY, nextX) in GetSurroundingSquares(y, x, octopuses.Length, octopuses[y].Length))
                                {
                                    if (hasFlashed.Contains((nextY, nextX))) continue;
                                    octopuses[nextY][nextX]++;
                                }
                            }
                        }
                    }
                } while (curFlashes != 0);
                if (hasFlashed.Count == octopuses.Length * octopuses[0].Length)
                {
                    return s.ToString();
                }
            }
        }
    }
}