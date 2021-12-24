using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day23
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
        var f = Utils.Utils.OpenInput("23");
        return "";
    }

    static string PartB()
    {
        var f = Utils.Utils.OpenInput("23");
        return "";
    }
}