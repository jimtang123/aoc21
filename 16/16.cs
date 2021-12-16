using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day16
{
    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => Do(true),
            "b" => Do(false),
            _   => null
        };
    }

    static (IEnumerable<char> remaining, long value, long versionSum) ReadLiteral(IEnumerable<char> s, int maxChars = 0, int maxPackets = 0)
    {
        long value = 0;
        while (s.Any())
        {
            var current = Convert.ToUInt32(string.Concat(s.Take(5)), 2);
            s = s.Skip(5);

            value = (value << 4) | (current & 0b1111);

            if ((current & 0b10000) == 0) break;
        }
        return (s, value, 0);
    }

    static (IEnumerable<char> remaining, long value, long versionSum) ReadOperator(IEnumerable<char> s, int which)
    {
        long resultFromType(long[] values)
        {
            return which switch
            {
                0 => values.Sum(),
                1 => values.Aggregate((long)1, (a, b) => a * b),
                2 => values.Min(),
                3 => values.Max(),
                5 => values[0] > values[1] ? 1 : 0,
                6 => values[0] < values[1] ? 1 : 0,
                7 => values[0] == values[1] ? 1 : 0,
                _ => 0,
            };
        };

        var lengthTypeId = s.Take(1).First();
        s = s.Skip(1);
        if (lengthTypeId == '0')
        {
            var totalBitLength = Convert.ToInt32(string.Concat(s.Take(15)), 2);
            s = s.Skip(15);
            var bits = s.Take(totalBitLength);
            var result = ReadBits(bits);
            return (s.Skip(totalBitLength), resultFromType(result.values), result.versionSum);
        } else 
        {
            var numberOfSubPackets = Convert.ToInt32(string.Concat(s.Take(11)), 2);
            s = s.Skip(11);
            var result = ReadBits(s, numberOfSubPackets);
            return (result.remaining, resultFromType(result.values), result.versionSum);
        }
    }

    static (IEnumerable<char> remaining, long[] values, long versionSum) ReadBits(IEnumerable<char> s, int maxPackets = -1)
    {
        long versionSum = 0;
        var values = new List<long>();
        while (s.Any() && !s.All((c) => c == '0'))
        {
            var version = Convert.ToUInt32(string.Concat(s.Take(3)), 2);
            versionSum += version;
            s = s.Skip(3);

            var type = Convert.ToInt32(string.Concat(s.Take(3)), 2);
            s = s.Skip(3);

            var next = type switch
            {
                4 => ReadLiteral(s),
                int other => ReadOperator(s, other)
            };
            values.Add(next.value);
            s = next.remaining;
            versionSum += next.versionSum;
            if (maxPackets != -1 && values.Count == maxPackets) break;
        }
        return (s, values.ToArray(), versionSum);
    }

    // Part A: 927
    // Part B: 1725277876501
    static string Do(bool justReturnVersionNumber)
    {
        var f = Utils.Utils.OpenInput("16");
        var remaining = f[0].SelectMany((c) => 
            Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')
        );
        var result = ReadBits(remaining);
        
        if (justReturnVersionNumber)
        {
            return result.versionSum.ToString();
        } else
        {
            return result.values[0].ToString();
        }
    }
}