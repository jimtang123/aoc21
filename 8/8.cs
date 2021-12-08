using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day8
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

        static string[] SortStringArray(IEnumerable<string> e)
        {
            return e
                .Select((s) =>
                    new String(s.OrderBy((c) => c).ToArray())
                )
                .ToArray();
        }

        // 421
        static string PartA()
        {
            var f = Utils.Utils.OpenInput("8");
            (string[] signal, string[] output)[] input = f
                .Select((line) =>
                {
                    var parts = line.Split(" | ");
                    return (parts[0].Split(" ").ToArray(), parts[1].Split(" ").ToArray());
                })
                .ToArray();
            return input
                .SelectMany((i) =>
                    i.output
                        .Where((o) => 
                            o.Length == 2
                            || o.Length == 3
                            || o.Length == 4
                            || o.Length == 8))
                .Count()
                .ToString();
        }

        // 986163
        static string PartB()
        {
            var f = Utils.Utils.OpenInput("8");
            (string[] signal, string[] output)[] input = f
                .Select((line) =>
                {
                    var parts = line.Split(" | ");
                    return (SortStringArray(parts[0].Split(" ")), SortStringArray(parts[1].Split(" ")));
                })
                .ToArray();
            return input
                .Select((i) =>
                {
                    var mapping = new Dictionary<string, int>();
                    var segmentForNumber = new Dictionary<int, string>();
                    var segmentsByLength = new Dictionary<int, HashSet<string>>();
                    foreach (var w in i.signal)
                    {
                        var len = w.Length;
                        if (!segmentsByLength.ContainsKey(len))
                        {
                            segmentsByLength[len] = new();
                        }
                        segmentsByLength[len].Add(w);
                    }
                    // The only 2 length segment
                    var segmentFor1 = segmentsByLength[2].First();
                    segmentsByLength[2].Remove(segmentFor1);
                    mapping[segmentFor1] = 1;
                    segmentForNumber[1] = segmentFor1;

                    // The only 3 length segment
                    var segmentFor7 = segmentsByLength[3].First();
                    segmentsByLength[3].Remove(segmentFor7);
                    mapping[segmentFor7] = 7;
                    segmentForNumber[7] = segmentFor7;

                    // The only 4 length segment
                    var segmentFor4 = segmentsByLength[4].First();
                    segmentsByLength[4].Remove(segmentFor4);
                    mapping[segmentFor4] = 4;
                    segmentForNumber[4] = segmentFor4;

                    // The only 7 length segment
                    var segmentFor8 = segmentsByLength[7].First();
                    segmentsByLength[7].Remove(segmentFor8);
                    mapping[segmentFor8] = 8;
                    segmentForNumber[8] = segmentFor8;

                    // 3 is the only 5 length segment that contains 1
                    var segmentFor3 = segmentsByLength[5]
                        .First((w) => segmentForNumber[1].All((c) => w.Contains(c)));
                    segmentsByLength[5].Remove(segmentFor3);
                    mapping[segmentFor3] = 3;
                    segmentForNumber[3] = segmentFor3;

                    // 9 is the only 6 length segment that contains 3
                    var segmentFor9 = segmentsByLength[6]
                        .First((w) => segmentForNumber[3].All((c) => w.Contains(c)));
                    segmentsByLength[6].Remove(segmentFor9);
                    mapping[segmentFor9] = 9;
                    segmentForNumber[9] = segmentFor9;

                    // 0 is the only 6 length segment that contains 7
                    var segmentFor0 = segmentsByLength[6]
                        .First((w) => segmentForNumber[7].All((c) => w.Contains(c)));
                    segmentsByLength[6].Remove(segmentFor0);
                    mapping[segmentFor0] = 0;
                    segmentForNumber[0] = segmentFor0;

                    // 6 is the last 6 length segment
                    var segmentFor6 = segmentsByLength[6].First();
                    segmentsByLength[6].Remove(segmentFor6);
                    mapping[segmentFor6] = 6;
                    segmentForNumber[6] = segmentFor6;

                    // 5 is the only 5 length that is inside 9
                    var segmentFor5 = segmentsByLength[5]
                        .First((w) => w.All((c) => segmentForNumber[9].Contains(c)));
                    segmentsByLength[5].Remove(segmentFor5);
                    mapping[segmentFor5] = 5;
                    segmentForNumber[5] = segmentFor5;

                    // 2 is the last 5 length segment
                    var segmentFor2 = segmentsByLength[5].First();
                    segmentsByLength[5].Remove(segmentFor2);
                    mapping[segmentFor2] = 2;
                    segmentForNumber[2] = segmentFor2;

                    // Return the value
                    return (
                        1000 * mapping[i.output[0]]
                        + 100 * mapping[i.output[1]]
                        + 10 * mapping[i.output[2]]
                        + mapping[i.output[3]]
                    );
                })
                .Sum()
                .ToString();
        }
    }
}