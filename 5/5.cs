using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day5
    {
        public static string? Run(string whichPart)
        {
            return whichPart switch
            {
                "a" => Do(false),
                "b" => Do(true),
                _   => null
            };
        }

        // A: 5169
        // B: 22083
        static string Do(bool considerDiagonals)
        {
            var f = Utils.Utils.OpenInput("5");
            var lines = new List<((int x, int y) start, (int x, int y) end)>();
            foreach (var line in f)
            {
                var points = line
                    .Split(" -> ")
                    .Select((p) =>
                    {
                        var parts = p
                            .Split(",")
                            .Select((n) => int.Parse(n))
                            .ToArray();
                        return (parts[0], parts[1]);
                    })
                    .ToArray();
                lines.Add((points[0], points[1]));
            }
            var x = lines
                .SelectMany((l) => new int[] {l.start.x, l.end.x})
                .Max() + 1;
            var y = lines
                .SelectMany((l) => new int[] {l.start.y, l.end.y})
                .Max() + 1;
            var graph = new int[y, x];
            foreach (var l in lines)
            {
                var x1 = l.start.x;
                var x2 = l.end.x;
                var y1 = l.start.y;
                var y2 = l.end.y;
                if (x1 == x2) {
                    for (int i = Math.Min(y1, y2); i <= Math.Max(y1, y2); i++)
                    {
                        graph[i,x1]++;
                    }
                } else if (y1 == y2)
                {
                    for (int i = Math.Min(x1, x2); i <= Math.Max(x1, x2); i++)
                    {
                        graph[y1,i]++;
                    }
                } else if (considerDiagonals && y1 < y2)
                {
                    if (x1 < x2)
                    {
                        for (int i = y1, j = x1; i <= y2 && j <= x2; i++, j++) {
                            graph[i,j]++;
                        }
                    } else
                    {
                        for (int i = y1, j = x1; i <= y2 && j >= x2; i++, j--) {
                            graph[i,j]++;
                        }
                    }
                } else if (considerDiagonals)
                {
                    if (x1 < x2)
                    {
                        for (int i = y1, j = x1; i >= y2 && j <= x2; i--, j++) {
                            graph[i,j]++;
                        }
                    } else
                    {
                        for (int i = y1, j = x1; i >= y2 && j >= x2; i--, j--) {
                            graph[i,j]++;
                        }
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (graph[i,j] >= 2) count++;
                }
            }
            return count.ToString();
        }
    }
}