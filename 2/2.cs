using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day2
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

        static (string, int) Read(string l)
        {
            var parts = l.Split(" ");
            return (parts[0], int.Parse(parts[1]));
        }

        static string PartA()
        {
            var f = Utils.Utils.OpenInput("2");
            int x = 0;
            int y = 0;
            foreach (var line in f)
            {
                (string m, int i) = Read(line);
                if (m == "forward")
                {
                    x += i;
                } else if (m == "up")
                {
                    y -= i;
                } else if (m == "down")
                {
                    y += i;
                }
            }
            return (x * y).ToString();
        }

        static string PartB()
        {
            var f = Utils.Utils.OpenInput("2");
            int x = 0;
            int y = 0;
            int aim = 0;
            foreach (var line in f)
            {
                (string m, int i) = Read(line);
                if (m == "forward")
                {
                    x += i;
                    y += aim * i;
                } else if (m == "up")
                {
                    aim -= i;
                } else if (m == "down")
                {
                    aim += i;
                }
            }
            return (x * y).ToString();
        }
    }
}