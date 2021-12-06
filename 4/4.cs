using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21
{
    public static class Day4
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

        static bool IsWinning(int[][] board) {
            for (int i = 0; i < 5; i++)
            {
                if (board[i].All((b) => b == 0)) return true;
                if (board.Select((b) => b[i]).All((b) => b == 0)) return true;
            }

            return false;
        }

        // A: 67716
        // B: 1830
        static string Do(bool findFirst)
        {
            var f = Utils.Utils.OpenInput("4");
            var numbers = f[0]
                .Split(",")
                .Select((n) => int.Parse(n));
            var boards = new List<int[][]>();
            for (int line = 2; line < f.Length;)
            {
                var board = new int[5][];
                for (int i = 0; i < 5; i++)
                {
                    board[i] = f[line]
                        .Split(" ")
                        .Where((n) => n != "")
                        .Select((n) => int.Parse(n))
                        .ToArray();
                    line++;
                }
                line++;
                boards.Add(board);
            }

            var winners = new HashSet<int>();
            foreach (var n in numbers)
            {
                for (int b = 0; b < boards.Count; b++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (boards[b][i][j] == n) {
                                boards[b][i][j] = 0;
                            }
                        }
                    }
                    if (IsWinning(boards[b]))
                    {
                        winners.Add(b);
                        if (findFirst || winners.Count == boards.Count)
                        {
                            var sum = boards[b]
                                .Select((b) => b.Sum())
                                .Sum();
                            return (sum * n).ToString();
                        }
                    }
                }
            }
            throw new Exception("No winning boards???");
        }
    }
}