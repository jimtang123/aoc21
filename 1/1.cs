using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public class Day1
    {
        public static string Run(string whichPart)
        {
            return whichPart switch
            {
                "a" => PartA(),
                "b" => PartB(),
                _   => "???"
            };
        }

        // 1715
        static string PartA()
        {
            var f = Utils.Utils.OpenInput("1");
            int cnt = 0;
            int last = int.Parse(f[0]);
            for (int i = 1; i < f.Count(); i++)
            {
                var cur = int.Parse(f[i]);
                if (cur > last) {
                    cnt++;
                }
                last = cur;
            }
            return cnt.ToString();
        }

        // 1739
        static string PartB()
        {
            var f = Utils.Utils.OpenInput("1")
                .Select((n) => int.Parse(n))
                .ToArray();
            int cnt = 0;
            var window = f
                .Take(3)
                .ToArray();
            for (int i = 3; i < f.Count(); i++)
            {
                if (f[i] > window[0]) {
                    cnt++;
                }
                window = window
                    .Skip(1)
                    .Append(f[i])
                    .ToArray();
            }
            return cnt.ToString();
        }
    }
}