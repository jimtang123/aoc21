using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day7
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

        // A: 344138
        // B: 94862124
        static string Do(bool useNonLinearFuel)
        {
            var f = Utils.Utils.OpenInput("7");
            var positions = f[0]
                .Split(",")
                .Select((n) => int.Parse(n))
                .ToArray();
            var minCost = Int32.MaxValue;
            foreach (var p in positions)
            {
                var cost = 0;
                foreach (var p2 in positions)
                {
                    var n = Math.Abs(p - p2);
                    cost += useNonLinearFuel
                        ? n * (1 + n) / 2 // Arithmetic sum
                        : n;
                }
                if (cost < minCost) minCost = cost;
            }
            return minCost.ToString();
        }
    }
}