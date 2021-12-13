using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day13
{
    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => Do(true, false),
            "b" => Do(false, true),
            _   => null
        };
    }

    // Part A: 655
    // Part B: JPZCUAUR
    static string Do(bool stopAtFirst, bool shouldPrintResult)
    {
        var f = Utils.Utils.OpenInput("13");
        var dots = new List<(int x, int y)>();
        var folds = new List<(char how, int where)>();
        var paper = new List<List<bool>>();
        var X = int.MinValue;
        var Y = int.MinValue;
        var finishedDots = false;
        foreach (var line in f)
        {
            if (line == "")
            {
                finishedDots = true;
                continue;
            }
            if (!finishedDots)
            {
                var parts = line
                    .Split(",")
                    .Select((n) => int.Parse(n))
                    .ToArray();
                dots.Add((parts[0], parts[1]));
                if (parts[0] > X) X = parts[0];
                if (parts[1] > Y) Y = parts[1];
            } else
            {
                var parts = line
                    .Split("=")
                    .ToArray();
                folds.Add((parts[0].Last(), int.Parse(parts[1])));
            }
        }
        if (stopAtFirst) folds = folds.Take(1).ToList();
        X++; Y++;
        for (int i = 0; i < Y; i++)
        {
            paper.Add(new List<bool>());
            for (int j = 0; j < X; j++)
            {
                paper[i].Add(false);
            }
        }
        foreach (var point in dots) paper[point.y][point.x] = true;

        foreach (var op in folds)
        {
            if (op.how == 'y')
            {
                for (int i = op.where + 1; i < Y; i++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        paper[2 * op.where - i][j] |= paper[i][j];
                    }
                }
                Y = op.where;
            } else if (op.how == 'x')
            {
                for (int i = 0; i < Y; i++)
                {
                    for (int j = op.where + 1; j < X; j++)
                    {
                        paper[i][2 * op.where  - j] |= paper[i][j];
                    }
                }
                X = op.where;
            }
        }

        var output = paper
            .Take(Y)
            .Select((row) => row
                .Take(X)
                .Count((b) => b))
            .Sum()
            .ToString();
        
        if (shouldPrintResult)
        {
            output += "\n" + string.Join('\n', paper
                .Take(Y)
                .Select((row) => string.Concat(row
                    .Take(X)
                    .Select((c) => c ? '\u2588' : ' '))));
        }

        return output;
    }
}