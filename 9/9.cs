using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day9
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

        static (int, int)[] NextPositions(int i, int j, int yLen, int xLen)
        {
            return new (int y, int x)[] { (i-1, j), (i+1, j), (i, j-1), (i, j+1) }
                .Where((p) => 
                    p.y >= 0
                    && p.y < yLen
                    && p.x >= 0
                    && p.x < xLen)
                .ToArray();
        }

        // 562
        static string PartA()
        {
            var f = Utils.Utils.OpenInput("9");
            var map = f
                .Select((r) => r
                    .Select((n) => n - '0')
                    .ToArray())
                .ToArray();
            return map
                .Select((row, i) => 
                    row
                        .Where((v, j) =>
                        {
                            foreach (var (y, x) in NextPositions(i, j, map.Length, map[0].Length))
                            {
                                if (v >= map[y][x]) return false;
                            }
                            return true;
                        })
                        .Select((v) => v + 1)
                        .Sum()
                )
                .Sum()
                .ToString();
        }

        static void Dfs(ref int[][] map, ref bool[][] visited, int i, int j, ref int size)
        {
            size++;
            visited[i][j] = true;
            foreach (var (y, x) in NextPositions(i, j, map.Length, map[0].Length))
            {
                if (!visited[y][x] && map[i][j] < map[y][x] && map[y][x] != 9)
                {
                    Dfs(ref map, ref visited, y, x, ref size);
                }
            }
        }

        // 1076922
        static string PartB()
        {
            var f = Utils.Utils.OpenInput("9");
            var map = f
                .Select((r) => r
                    .Select((n) => n - '0')
                    .ToArray())
                .ToArray();
            var basinSizes = new List<int>();
            var lowPoint = new bool[map.Length][];
            var visited = new bool[map.Length][];
            for (int i = 0; i < map.Length; i++)
            {
                lowPoint[i] = new bool[map[0].Length];
                visited[i] = new bool[map[0].Length];
                for (int j = 0; j < map[0].Length; j++)
                {
                    var isLow = true;
                    foreach (var (y, x) in NextPositions(i, j, map.Length, map[0].Length))
                    {
                        if (map[i][j] >= map[y][x])
                        {
                            isLow = false;
                            break;
                        }
                    }
                    lowPoint[i][j] = isLow;
                    visited[i][j] = false;
                }
            }
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    if (lowPoint[i][j] && !visited[i][j])
                    {
                        int size = 0;
                        Dfs(ref map, ref visited, i, j, ref size);
                        basinSizes.Add(size);
                    }
                }
            }
            return basinSizes
                .OrderByDescending((v) => v)
                .Take(3)
                .Aggregate(1, (v1, v2) => v1 * v2)
                .ToString();
        }
    }
}