using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day3
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

        // 2583164
        static string PartA()
        {
            var f = Utils.Utils.OpenInput("3");
            int[,] counts = new int[64,2];
            for (int i = 0; i < 64; i++) counts[i, 0] = counts[i, 1] = 0;
            foreach (var line in f)
            {
                for (int i = 0; i < line.Length; i++) counts[i, line[i] - '0']++;
            }
            var gamma = 0;
            var epsilon = 0;
            for (int i = 0; i < f[0].Length; i++)
            {
                if (counts[i, 0] > counts[i, 1])
                {
                    gamma = gamma << 1;
                    epsilon = (epsilon << 1) | 1;
                } else
                {
                    gamma = (gamma << 1) | 1;
                    epsilon = epsilon << 1;
                }
            }
            return (gamma * epsilon).ToString();
        }

        // 2784375
        static string PartB()
        {
            var f = Utils.Utils.OpenInput("3");
            var generators = new List<string>(f);
            var scrubbers = new List<string>(f);
            for (int i = 0; i < f[0].Length && generators.Count > 1; i++)
            {
                int[] counts = new int[2] {0, 0};
                foreach (var w in generators)
                {
                    counts[w[i] - '0']++;
                }
                if (counts[0] > counts[1])
                {
                    generators = new(
                        generators.Where((s) =>
                            s[i] == '0')
                    );
                } else {
                    generators = new(
                        generators.Where((s) =>
                            s[i] == '1')
                    );
                }
            }
            for (int i = 0; i < f[0].Length && scrubbers.Count > 1; i++)
            {
                int[] counts = new int[2] {0, 0};
                foreach (var w in scrubbers)
                {
                    counts[w[i] - '0']++;
                }
                if (counts[0] > counts[1])
                {
                    scrubbers = new(
                        scrubbers.Where((s) =>
                            s[i] == '1')
                    );
                } else {                    
                    scrubbers = new(
                        scrubbers.Where((s) =>
                            s[i] == '0')
                    );
                }
            }
            return (Convert.ToInt32(generators[0], 2) * Convert.ToInt32(scrubbers[0], 2)).ToString();
        }
    }
}