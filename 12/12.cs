using System;
using System.Collections.Generic;
using System.Linq;
using Utils;

namespace Aoc21;

public static class Day12
{
    class Graph
    {
        Dictionary<string, int> CaveNames = new();
        Dictionary<int, string> VertexToCaveName = new();
        Dictionary<int, bool> IsBig = new();
        int NumCaves = 0;
        List<List<bool>> Map = new();

        public Graph(string[] input)
        {
            // Get vertices
            foreach (var conn in input)
            {
                var caves = conn.Split("-");
                foreach (var cave in caves)
                {
                    if (!CaveNames.ContainsKey(cave))
                    {
                        CaveNames[cave] = NumCaves;
                        VertexToCaveName[NumCaves] = cave;
                        IsBig[NumCaves] = cave.All(char.IsUpper);
                        NumCaves++;
                    }
                }
            }
            for (int i = 0; i < NumCaves; i++)
            {
                Map.Add(new());
                for (int j = 0; j < NumCaves; j++)
                {
                    Map[i].Add(false);
                }
            }

            foreach (var conn in input)
            {
                var caves = conn.Split("-");
                Map[CaveNames[caves[0]]][CaveNames[caves[1]]] = true;
                Map[CaveNames[caves[1]]][CaveNames[caves[0]]] = true;
            }
        }

        class BfsState
        {
            public int Cur;
            public List<int> Path;
            public HashSet<int> Visited;
            public bool CanVisitSmallCaveTwice;
            public BfsState(int cur, List<int> path, bool canVisitSmallCaveTwice)
            {
                Cur = cur;
                Path = new(path);
                Path.Add(cur);
                Visited = new(Path);
                CanVisitSmallCaveTwice = canVisitSmallCaveTwice;
            }
        }

        public List<string[]> BfsFromStart(bool canVisitSmallCaveTwice = false)
        {
            var paths = new List<string[]>();
            var q = new Queue<BfsState>();
            q.Enqueue(new BfsState(CaveNames["start"], new List<int>(0), canVisitSmallCaveTwice));
            while (q.Count != 0)
            {
                var state = q.Dequeue();
                if (state.Cur == CaveNames["end"])
                {
                    paths.Add(state.Path.Select((p) => VertexToCaveName[p]).ToArray());
                    continue;
                }
                for (int next = 0; next < NumCaves; next++)
                {
                    if (state.Cur == next) continue;
                    if (!Map[state.Cur][next]) continue;
                    if (!IsBig[next] && state.Visited.Contains(next))
                    {
                        if (next == CaveNames["start"] || next == CaveNames["end"]) continue;
                        if (!state.CanVisitSmallCaveTwice) continue;
                        q.Enqueue(new BfsState(next, state.Path, false));
                    } else
                    {
                        q.Enqueue(new BfsState(next, state.Path, state.CanVisitSmallCaveTwice));
                    }
                }
            }
            return paths;
        }
    }

    public static string? Run(string whichPart)
    {
        return whichPart switch
        {
            "a" => PartA(),
            "b" => PartB(),
            _   => null
        };
    }

    // 3450
    static string PartA()
    {
        var f = Utils.Utils.OpenInput("12");
        var g = new Graph(f);
        return g.BfsFromStart().Count.ToString();
    }

    // 96528
    static string PartB()
    {
        var f = Utils.Utils.OpenInput("12");
        var g = new Graph(f);
        return g.BfsFromStart(true).Count.ToString();
    }
}