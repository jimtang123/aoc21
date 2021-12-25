using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day25
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

    // 367
    static string PartA()
    {
        var f = Utils.Utils.OpenInput("25");
        var xLen = f[0].Length;
        var yLen = f.Length;
        var state = f.Select((l) => l.Select((c) => c).ToArray()).ToArray();
        bool changed = true;
        var steps = 0;
        while (changed)
        {
            changed = false;
            steps++;
            var nextState = new char[yLen][];
            for (var i = 0; i < yLen; i++)
            {
                nextState[i] = new char[xLen];
                for (var j = 0; j < xLen; j++)
                {
                    nextState[i][j] = '.';
                }
            }
            for (int y = 0; y < yLen; y++)
            {
                for (int x = 0; x < xLen; x++)
                {
                    if (state[y][x] != '>') continue;
                    
                    if (state[y][(x + 1) % xLen] == '.')
                    {
                        nextState[y][(x + 1) % xLen] = '>';
                        changed = true;
                    } else
                    {
                        nextState[y][x] = '>';
                    }
                }
            }
            for (int y = 0; y < yLen; y++)
            {
                for (int x = 0; x < xLen; x++)
                {
                    if (state[y][x] != 'v') continue;
                    
                    if (state[(y + 1) % yLen][x] != 'v' && nextState[(y + 1) % yLen][x] == '.')
                    {
                        nextState[(y + 1) % yLen][x] = 'v';
                        changed = true;
                    } else
                    {
                        nextState[y][x] = 'v';
                    }
                }
            }
            state = nextState;
        }
        return steps.ToString();
    }

    static string PartB()
    {
        var f = Utils.Utils.OpenInput("25");
        return "";
    }
}