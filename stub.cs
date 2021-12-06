using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class DayDAY_NUMBER
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

        static string PartA()
        {
            var f = Utils.Utils.OpenInput("DAY_NUMBER");
        }

        static string PartB()
        {
            var f = Utils.Utils.OpenInput("DAY_NUMBER");
        }
    }
}