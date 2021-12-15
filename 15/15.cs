using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day15
{
    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => Do(1),
            "b" => Do(5),
            _ => null
        };
    }

    static IEnumerable<(int x, int y)> GetNextPosition(int curX, int curY, int yLen, int xLen, int nTilings)
    {
        yLen *= nTilings;
        xLen *= nTilings;
        foreach (var (i, j) in new (int, int)[] { (curX - 1, curY), (curX + 1, curY), (curX, curY - 1), (curX, curY + 1) })
        {
            if (i < 0
                || i >= xLen
                || j < 0
                || j >= yLen) continue;

            yield return (i, j);
        }
    }

    // Part A: 472
    // Part B: 2851
    static string Do(int nTilings)
    {
        var f = Utils.Utils.OpenInput("15");
        var map = f.Select((line) => line.Select((c) => c - '0').ToArray()).ToArray();
        var q = new PriorityQueue<(int x, int y, int danger), int>();
        q.Enqueue((0, 0, 0), 0);
        var minDangers = new List<List<int>>();
        for (int i = 0; i < map.Length * nTilings; i++)
        {
            minDangers.Add(new());
            for (int j = 0; j < map[0].Length * nTilings; j++)
            {
                minDangers[i].Add(int.MaxValue);
            }
        }
        var seen = new HashSet<(int, int)>();
        while (q.Count > 0)
        {
            var cur = q.Dequeue();
            seen.Remove((cur.x, cur.y));

            foreach (var next in GetNextPosition(cur.x, cur.y, map.Length, map[0].Length, nTilings))
            {
                var mapX = next.x % map[0].Length;
                var mapY = next.y % map.Length;
                var toIncrease = (next.x / map[0].Length) + (next.y / map.Length);
                var mapDanger = 1 + ((map[mapY][mapX] + toIncrease - 1) % 9);
                var danger = cur.danger + mapDanger;
                if (danger < minDangers[next.y][next.x])
                {
                    minDangers[cur.y][cur.x] = cur.danger;
                    // This works because we are assuming every new step in the path is additive
                    if (seen.Add((next.x, next.y)))
                        q.Enqueue((next.x, next.y, danger), danger);
                }

            }
        }
        return minDangers[nTilings * map.Length - 1][nTilings * map[0].Length - 1].ToString();
    }
}