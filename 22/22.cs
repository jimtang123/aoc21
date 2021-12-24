using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day22
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

    // 580098
    static string PartA()
    {
        var f = Utils.Utils.OpenInput("22");
        var on = new Dictionary<int, Dictionary<int, Dictionary<int, bool>>>();
        var instructions = f
            .Select<string, (bool op, (int from, int to) x, (int from, int to) y, (int from, int to) z)>((line) =>
        {
            var op = line[1] == 'f' ? false : true;
            var parts = line
                .Split(",")
                .Select((part) => part.Split("=")[1])
                .Select((part) => {
                    var points = part.Split("..");
                    return (int.Parse(points[0])!, int.Parse(points[1])!);
                })
                .ToArray();
            return (op, parts[0], parts[1], parts[2]);
        });

        foreach (var r in instructions)
        {
            if (Math.Abs(r.x.from) > 50
                || Math.Abs(r.x.to) > 50
                || Math.Abs(r.y.from) > 50
                || Math.Abs(r.y.to) > 50
                || Math.Abs(r.z.from) > 50
                || Math.Abs(r.z.to) > 50) continue;
            
            for (int x = r.x.from; x <= r.x.to; x++)
            {
                if (!on.TryGetValue(x, out var ySet))
                {
                    ySet = (on[x] = new());
                }
                for (int y = r.y.from; y <= r.y.to; y++)
                {
                    if (!ySet.TryGetValue(y, out var zSet))
                    {
                        zSet = (ySet[y] = new());
                    }
                    for (int z = r.z.from; z <= r.z.to; z++)
                    {
                        zSet[z] = r.op;
                    }
                }
            }
        }
        int nOn = 0;
        foreach (var (_, sY) in on)
        {
            foreach (var (_, sZ) in sY)
            {
                foreach (var (_, zV) in sZ)
                {
                    if (zV) nOn++;
                }
            }
        }
        return nOn.ToString();
    }

    static string PartB()
    {
        var f = Utils.Utils.OpenInput("22");
        var instructions = f
            .Select<string, (bool op, (int from, int to) x, (int from, int to) y, (int from, int to) z)>((line) =>
        {
            var op = line[1] == 'f' ? false : true;
            var parts = line
                .Split(",")
                .Select((part) => part.Split("=")[1])
                .Select((part) => {
                    var points = part.Split("..");
                    return (int.Parse(points[0])!, int.Parse(points[1])!);
                })
                .ToArray();
            return (op, parts[0], parts[1], parts[2]);
        });

        var on = new HashSet<((int from, int to) x, (int from, int to) y, (int from, int to) z)>();
        foreach (var ins in instructions)
        {
            if (ins.op)
            {
                
            } else
            {

            }
        }

        return "";
    }
}