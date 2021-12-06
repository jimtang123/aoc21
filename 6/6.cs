using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day6
    {
        public static string? Run(string whichPart)
        {
            return whichPart switch
            {
                "a" => Do(80),
                "b" => Do(256),
                _   => null
            };
        }

        // A: 383160
        // B: 1721148811504
        static string Do(int nDays)
        {
            var f = Utils.Utils.OpenInput("6");
            var daysLeft = new long[9];
            foreach (var fish in f[0].Split(",").Select((fi) => long.Parse(fi))) daysLeft[fish]++;

            while (nDays-- > 0)
            {
                var newFish = daysLeft[0];
                daysLeft = daysLeft
                    .Skip(1)
                    .Append(newFish)
                    .ToArray();
                daysLeft[6] += newFish;
            }
            return daysLeft.Sum().ToString();
        }
    }
}