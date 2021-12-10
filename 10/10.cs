using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day10
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

        static Dictionary<char, int> PointsForPartA = new()
        {
            [')'] = 3,
            [']'] = 57,
            ['}'] = 1197,
            ['>'] = 25137,
        };

        static Dictionary<char, int> PointsForPartB = new()
        {
            [')'] = 1,
            [']'] = 2,
            ['}'] = 3,
            ['>'] = 4,
        };

        static bool IsOpeningBracket(char b) =>
            b == '(' || b == '[' || b == '{' || b == '<';
        
        static char GetMatchingOpeningBracket(char b) =>
            b switch
            {
                ')' => '(',
                ']' => '[',
                '}' => '{',
                '>' => '<',
                _ => b
            };
        
        static char GetMatchingClosingBracket(char b) =>
            b switch
            {
                '(' => ')',
                '[' => ']',
                '{' => '}',
                '<' => '>',
                _ => b
            };
        
        // 392139
        static string PartA()
        {
            var f = Utils.Utils.OpenInput("10");
            return f.Select((line) => {
                var stack = new Stack<char>();
                var firstCorrupted = line.FirstOrDefault((b) =>
                {
                    if (IsOpeningBracket(b))
                    {
                        stack.Push(b);
                    } else
                    {
                        var top = stack.Pop(); // All cases start with an opening bracket
                        if (top != GetMatchingOpeningBracket(b))
                        {
                            return true;
                        }
                    }
                    return false;
                });
                // Incomplete line
                if (firstCorrupted == default(char)) return 0;

                return PointsForPartA[firstCorrupted];
            })
            .Sum()
            .ToString();
        }

        // 4001832844
        static string PartB()
        {
            var f = Utils.Utils.OpenInput("10");
            var scores = f
                .Select((line) => {
                    var stack = new Stack<char>();
                    var firstCorrupted = line.FirstOrDefault((b) =>
                    {
                        if (IsOpeningBracket(b))
                        {
                            stack.Push(b);
                        } else
                        {
                            var top = stack.Pop(); // All cases start with an opening bracket
                            if (top != GetMatchingOpeningBracket(b))
                            {
                                return true;
                            }
                        }
                        return false;
                    });
                    // Corrupted line
                    if (firstCorrupted != default(char)) return 0;

                    return stack.Aggregate((long)0, (prev, remaining) => 
                        (5 * prev) + PointsForPartB[GetMatchingClosingBracket(remaining)]
                    );
                })
                .Where((s) => s != 0)
                .OrderBy((s) => s);
            return scores
                .Skip(scores.Count() / 2)
                .First()
                .ToString();
        }
    }
}