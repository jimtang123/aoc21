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
                "a" => PartA(),
                "b" => PartB(),
                _   => null
            };
        }

        static bool IsWinning(int[,] board) {
            for (int i = 0; i < 5; i++)
            {
                if (board[i,0] + board[i,1] + board[i,2] + board[i,3] + board[i,4] == 0) return true;
                if (board[0,i] + board[1,i] + board[2,i] + board[3,i] + board[4,i] == 0) return true;
            }

            return false;
        }

        static string Done(int[,] board, int n) {
            var sum = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    sum += board[i,j];
                }
            }
            return (n * sum).ToString();
        }

        static string PartA()
        {
            var f = Utils.Utils.OpenInput("4");
            var numbers = f[0]
                .Split(",")
                .Select((n) => int.Parse(n));
            var boards = new List<int[,]>();
            for (int line = 2; line < f.Length;)
            {
                var board = new int[5,5];
                for (int i = 0; i < 5; i++)
                {
                    var n = f[line]
                        .Split(" ")
                        .Where((n) => n != "")
                        .Select((n) => int.Parse(n))
                        .ToArray();
                    for (int j = 0; j < 5; j++)
                    {
                        board[i,j] = n[j];
                    }
                    line++;
                }
                line++;
                boards.Add(board);
            }

            foreach (var n in numbers)
            {
                for (int b = 0; b < boards.Count; b++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            if (boards[b][i,j] == n) {
                                boards[b][i,j] = 0;
                            }
                        }
                    }
                    if (IsWinning(boards[b])) {
                        return Done(boards[b], n);
                    }
                }
            }
            return "???";
        }

        static string PartB()
        {
            var f = Utils.Utils.OpenInput("4");
            var numbers = f[0]
                .Split(",")
                .Select((n) => int.Parse(n));
            var boards = new List<int[,]>();
            for (int line = 2; line < f.Length;)
            {
                var board = new int[5,5];
                for (int i = 0; i < 5; i++)
                {
                    var n = f[line]
                        .Split(" ")
                        .Where((n) => n != "")
                        .Select((n) => int.Parse(n))
                        .ToArray();
                    for (int j = 0; j < 5; j++)
                    {
                        board[i,j] = n[j];
                    }
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
                            if (boards[b][i,j] == n) {
                                boards[b][i,j] = 0;
                            }
                        }
                    }
                    if (IsWinning(boards[b])) {
                        winners.Add(b);
                        if (winners.Count == boards.Count)
                        {
                            return Done(boards[b], n);
                        }
                    }
                }
            }
            return "???";
        }
    }
}