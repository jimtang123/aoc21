using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day17
{
    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => Do().highest,
            "b" => Do().hits,
            _   => null
        };
    }

    // Part A: 5460
    // Part B: 3618
    static (string highest, string hits) Do()
    {
        var f = Utils.Utils.OpenInput("17");
        var points = string
            .Concat(f[0].Skip(13))
            .Split(", ")
            .Select<string, (int min, int max)>((x) =>
            {
                var ns = x.Split("=")[1].Split("..");
                return (int.Parse(ns[0]), int.Parse(ns[1]));
            })
            .ToArray();
        var xRange = points[0];
        var yRange = points[1];
        long highest = int.MinValue;
        long nHits = 0;
        // Range of y and x define max and min possible to reach area
        for (int vY = yRange.min; vY <= Math.Abs(yRange.min); vY++)
        {
            for (int vX = 1; vX <= xRange.max; vX++)
            {
                var x = 0;
                var y = 0;
                var curVy = vY;
                var curVx = vX;
                var curMaxHeight = int.MinValue;
                for (int t = 1;; t++)
                {
                    x += curVx;
                    y += curVy;
                    curVy--;
                    curVx = Math.Max(curVx - 1, 0);
                    if (curVy == 0) curMaxHeight = y;
                    if (x >= xRange.min
                        && x <= xRange.max
                        && y >= yRange.min
                        && y <= yRange.max)
                    {
                        highest = Math.Max(curMaxHeight, highest);
                        nHits++;
                        break;
                    } else if ((x > xRange.max) || y < yRange.min) break;
                }
            }
        }
        return (highest.ToString(), nHits.ToString());
    }
}