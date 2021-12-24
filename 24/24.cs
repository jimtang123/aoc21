using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day24
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

    static bool IsDigit(string s) =>
        s.All((c) => c == '-' || (c <= '9' && c >= '0'));
    
    static int ParseVars(Dictionary<string, int> vars, string val) =>
        IsDigit(val)
            ? int.Parse(val)
            : vars[val];
    

    static string PartA()
    {
        var f = Utils.Utils.OpenInput("24");
        int c = 0;
        var instructionStart = new Dictionary<int, int>();
        for (int i = 0; i < f.Length; i++)
        {
            if (f[i] == "inp w") instructionStart[c++] = i;
        }
        var digits = new List<int>();
        for (int i = 0; i < 14; i++) digits.Add(0);

        var vars = new Dictionary<string, int>()
        {
            ["w"] = 0,
            ["x"] = 0,
            ["y"] = 0,
            ["z"] = 0
        };
        for (int digit = 0; digit < 14; digit++)
        {
            for (int w = 9; w > 0; w--)
            {
                var curVars = new Dictionary<string, int>()
                {
                    ["w"] = w,
                    ["x"] = vars["x"],
                    ["y"] = vars["y"],
                    ["z"] = vars["z"]
                };
                for (int i = instructionStart[digit] + 1; i < f.Length && f[i] != "inp w"; i++)
                {
                    var instruction = f[i].Split(" ").ToArray();
                    switch (instruction[0])
                    {
                        case "add":
                            Console.WriteLine($"{f[i]} -> {instruction[1]}:{curVars[instruction[1]]} = {curVars[instruction[1]] + ParseVars(curVars, instruction[2])}");
                            curVars[instruction[1]] = curVars[instruction[1]] + ParseVars(curVars, instruction[2]);
                            break;
                        case "mul":
                            Console.WriteLine($"{f[i]} -> {instruction[1]}:{curVars[instruction[1]]} = {curVars[instruction[1]] * ParseVars(curVars, instruction[2])}");
                            curVars[instruction[1]] = curVars[instruction[1]] * ParseVars(curVars, instruction[2]);
                            break;
                        case "div":
                            Console.WriteLine($"{f[i]} -> {instruction[1]}:{curVars[instruction[1]]} = {curVars[instruction[1]] / ParseVars(curVars, instruction[2])}");
                            curVars[instruction[1]] = curVars[instruction[1]] / ParseVars(curVars, instruction[2]);
                            break;
                        case "mod":
                            Console.WriteLine($"{f[i]} -> {instruction[1]}:{curVars[instruction[1]]} = {curVars[instruction[1]] % ParseVars(curVars, instruction[2])}");
                            curVars[instruction[1]] = curVars[instruction[1]] % ParseVars(curVars, instruction[2]);
                            break;
                        case "eql":
                            Console.WriteLine($"{f[i]} -> {instruction[1]}:{curVars[instruction[1]]} = {(curVars[instruction[1]] == ParseVars(curVars, instruction[2]) ? 1 : 0)}");
                            curVars[instruction[1]] = curVars[instruction[1]] == ParseVars(curVars, instruction[2])
                                ? 1
                                : 0;
                            break;
                    }
                }
                Console.WriteLine(curVars["z"]);
                if (curVars["z"] == 0)
                {
                    vars["x"] = curVars["x"];
                    vars["y"] = curVars["y"];
                    vars["z"] = curVars["z"];
                    digits[digit] = w;
                    break;
                }
            }
        }
        return string.Join("", digits);
    }

    static string PartB()
    {
        var f = Utils.Utils.OpenInput("24");
        return "";
    }
}