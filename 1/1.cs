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

        static string PartA()
        {
            var f = Utils.Utils.OpenInput("1");
            int cnt = 0;
            int last = int.Parse(f[0]);
            int cur;
            for (int i = 1; i < f.Count(); i++)
            {
                cur = int.Parse(f[i]);
                if (cur > last) {
                    cnt++;
                }
                last = cur;
            }
            return cnt.ToString();
        }

        static string PartB()
        {
            var f = Utils.Utils.OpenInput("1");
            int cnt = 0;
            var window = new int[3];
            int cur;
            window[0] = int.Parse(f[0]);
            window[1] = int.Parse(f[1]);
            window[2] = int.Parse(f[2]);
            for (int i = 3; i < f.Count(); i++)
            {
                cur = int.Parse(f[i]);
                if (cur > window[0]) {
                    cnt++;
                }
                window[0] = window[1];
                window[1] = window[2];
                window[2] = cur;
            }
            return cnt.ToString();
        }
    }
}