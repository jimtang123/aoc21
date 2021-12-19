using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day19
{
    public struct Point
    {
        public Point(int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
        public int x;
        public int y;
        public int z;
    }
    public static Point[] Facings = new Point[]
    {
        new Point(1, 0, 0),
        new Point(-1, 0, 0),
        new Point(0, 1, 0),
        new Point(0, -1, 0),
        new Point(0, 0, 1),
        new Point(0, 0, -1),
    };

    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => PartA(),
            "b" => PartB(),
            _   => null
        };
    }

    // Returns points of b rotated to match the same plane as a or null if no matching is found
    static List<Point>? CheckScannerCommonAndRotate(List<Point> a, List<Point> b)
    {
        var aPoints = new HashSet<Point>(a);
        for (int rotX = 0; rotX < 4; rotX++)
        {
            for (int rotY = 0; rotY < 4; rotY++)
            {
                for (int rotZ = 0; rotZ < 4; rotZ++)
                {
                    
                }
            }
        }
        return null;
    }

    static string PartA()
    {
        var f = Utils.Utils.OpenInput("19");
        List<List<Point>> scanners = new();
        List<List<Point>?> goodScanners = new();
        List<bool> finishedScanners = new();
        for (int line = 0; line < f.Length; line++)
        {
            goodScanners.Add(null);
            finishedScanners.Add(false);
            scanners.Add(new());
            line++;
            while (line < f.Length && f[line].Length != 0)
            {
                var v = f[line]
                    .Split(",")
                    .Select((i) => int.Parse(i))
                    .ToArray();
                scanners.Last().Add(new Point(v[0], v[1], v[2]));
                line++;
            }
        }
        // Assume the first scanner is ok
        goodScanners[0] = scanners[0];
        while (!finishedScanners.All((s) => s))
        {
            for (int i = 0; i < scanners.Count; i++)
            {
                if (goodScanners[i] == null || finishedScanners[i]) continue;
                for (int j = 0; j < scanners.Count; j++)
                {
                    if (i == j || goodScanners[j] != null) continue;

                    var commonScanner = CheckScannerCommonAndRotate(goodScanners[i]!, scanners[j]);
                    if (commonScanner != null)
                    {
                        goodScanners[j] = commonScanner;
                    }
                }
                finishedScanners[i] = true;
            }
        }
        return "";
    }

    static string PartB()
    {
        var f = Utils.Utils.OpenInput("19");
        return "";
    }
}