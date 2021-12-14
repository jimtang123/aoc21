using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day14
{
    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => Do(10),
            "b" => Do(40),
            _ => null
        };
    }

    // Part A: 2967
    // Part B: 3692219987038
    static string Do(int nSteps)
    {
        var f = Utils.Utils.OpenInput("14");
        var template = f[0];
        var rules = new Dictionary<string, char>(f
            .Skip(2)
            .Select((line) =>
            {
                var parts = line.Split(" -> ");
                return new KeyValuePair<string, char>(parts[0], parts[1][0]);
            }));
        var childPairsToParentPairs = new Dictionary<string, HashSet<string>>(); // from created pairs, which parents can get you there
        var pairCounts = new Dictionary<string, long>(); // single-level DP array
        foreach (var (p, n) in rules)
        {
            if (!childPairsToParentPairs.ContainsKey(p)) childPairsToParentPairs[p] = new();
            if (!pairCounts.ContainsKey(p)) pairCounts[p] = 0;

            for (int i = 0; i < template.Length - 1; i++)
            {
                if (string.Concat(template.Skip(i).Take(2)) == p) pairCounts[p]++;
            }

            var first = string.Concat(p[0], n);
            if (!childPairsToParentPairs.ContainsKey(first)) childPairsToParentPairs[first] = new();
            childPairsToParentPairs[first].Add(p);

            var second = string.Concat(n, p[1]);
            if (!childPairsToParentPairs.ContainsKey(second)) childPairsToParentPairs[second] = new();
            childPairsToParentPairs[second].Add(p);
        }

        for (int step = 0; step < nSteps; step++)
        {
            var newPairCounts = new Dictionary<string, long>();
            foreach (var rule in rules)
            {
                newPairCounts[rule.Key] = childPairsToParentPairs[rule.Key].Select((p) => pairCounts[p]).Sum();
            }
            pairCounts = newPairCounts;
        }

        var charCounts = new Dictionary<char, long>();
        foreach (var kvp in pairCounts)
        {
            if (!charCounts.ContainsKey(kvp.Key[0])) charCounts[kvp.Key[0]] = kvp.Value;
            else charCounts[kvp.Key[0]] += kvp.Value;

            if (!charCounts.ContainsKey(kvp.Key[1])) charCounts[kvp.Key[1]] = kvp.Value;
            else charCounts[kvp.Key[1]] += kvp.Value;
        }
        // Everything counted twice except first two letters
        charCounts[template.First()]++;
        charCounts[template.Last()]++;

        var difference = charCounts.Max((kvp) => kvp.Value / 2) - charCounts.Min((kvp) => kvp.Value / 2);
        return difference.ToString();
    }
}