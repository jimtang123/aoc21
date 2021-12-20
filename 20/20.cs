using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day20
{
    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => Do(2),
            "b" => Do(50),
            _   => null
        };
    }

    static IEnumerable<(int x, int y)> Neighbours(int x, int y)
    {
        yield return (x-1, y-1);
        yield return (x, y-1);
        yield return (x+1, y-1);
        yield return (x-1, y);
        yield return (x, y);
        yield return (x+1, y);
        yield return (x-1, y+1);
        yield return (x, y+1);
        yield return (x+1, y+1);
    }

    static int DefaultAlwaysDark(int iter)
        => 0;
    static int DefaultAlwaysLightAfterFirst(int iter)
        => iter == 0 ? 0 : 1;
    static int DefaultFlippingLightDark(int iter)
        => iter % 2 == 1 ? 1 : 0;

    // Part A: 5044
    // Part B: 18074
    static string Do(int nIters)
    {
        var f = Utils.Utils.OpenInput("20");
        var algorithm = f[0].Select((c) => c == '#' ? 1 : 0).ToArray();
        var image = new List<List<int>>();

        // Figure out the defaultValue function for determining what the outer pixels are
        Func<int, int> defaultValue;
        if (algorithm[0] == '.')
        {
            defaultValue = DefaultAlwaysDark;
        } else
        {
            if (algorithm.Last() == '#')
            {
                defaultValue = DefaultAlwaysLightAfterFirst;
            } else
            {
                defaultValue = DefaultFlippingLightDark;
            }
        }

        foreach (var line in f.Skip(2))
        {
            image.Add(line.Select((c) => c == '#' ? 1 : 0).ToList());
        }
        for (int iter = 0; iter < nIters; iter++)
        {
            int Get(int x, int y)
            {
                if (x < 0
                    || x >= image.Count
                    || y < 0
                    || y >= image.Count) return defaultValue(iter);
                return image[y][x];
            };
            var newImage = new List<List<int>>();
            
            for (int j = -1; j < image.Count + 1; j++)
            {
                newImage.Add(new());
                for (int i = -1; i < image[0].Count + 1; i++)
                {
                    newImage[j + 1]
                        .Add(algorithm[Neighbours(i, j)
                            .Select((v) => Get(v.x, v.y))
                            .Aggregate(0, (prev, cur) => (prev << 1) | cur)]);
                }
            }
            image = newImage;
        }
        return image
            .Select((line) => line.Sum())
            .Sum()
            .ToString();
    }
}