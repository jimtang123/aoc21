using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day21
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

    // 1067724
    static string PartA()
    {
        var f = Utils.Utils.OpenInput("21");
        var p1Pos = f[0].Last() - '0';
        if (p1Pos == 0) p1Pos = 10;
        var p2Pos = f[1].Last() - '0';
        if (p2Pos == 0) p2Pos = 10;

        var dice = 1;
        var p1Score = 0;
        var p2Score = 0;
        var isPlayer1Turn = true;

        while (p1Score < 1000 && p2Score < 1000)
        {
            var move = (3 * dice + 3) % 10;

            if (isPlayer1Turn)
            {
                p1Pos = 1 + (p1Pos - 1 + move) % 10;
                p1Score += p1Pos;
            } else
            {
                p2Pos = 1 + (p2Pos - 1 + move) % 10;
                p2Score += p2Pos;
            }
            dice += 3;
            isPlayer1Turn = !isPlayer1Turn;
        }

        if (isPlayer1Turn)
            return (p1Score * (dice - 1)).ToString();
        else
            return (p2Score * (dice - 1)).ToString();
        
    }

    static Dictionary<int, int> FreqHistogram = new()
    {
        [3] = 1, // 111
        [4] = 3, // 112, 121, 211
        [5] = 6, // 122, 212, 221, 113, 131, 311
        [6] = 7, // 222, 123, 213, 132, 231, 321, 312
        [7] = 6, // 133, 313, 331, 223, 232, 322
        [8] = 3, // 233, 323, 332
        [9] = 1, // 333
    };

    static (long wins, long losses) getUniverses(int curScore, int otherScore, int curPos, int otherPos)
    {
        if (otherScore >= 21) return (0, 1);
        var wins = 0L;
        var losses = 0L;
        foreach (var kv in FreqHistogram)
        {
            var newPos = 1 + (curPos - 1 + kv.Key) % 10;
            var next = getUniverses(otherScore, curScore + newPos, otherPos, newPos);
            wins += next.losses * kv.Value;
            losses += next.wins * kv.Value;
        }
        return (wins, losses);
    }

    // 630947104784464
    static string PartB()
    {
        var f = Utils.Utils.OpenInput("21");
        
        var p1Pos = f[0].Last() - '0';
        if (p1Pos == 0) p1Pos = 10;
        var p2Pos = f[1].Last() - '0';
        if (p2Pos == 0) p2Pos = 10;

        var result = getUniverses(0, 0, p1Pos, p2Pos);
        return Math.Max(result.wins, result.losses).ToString();
    }
}