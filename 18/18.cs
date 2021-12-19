using System;
using System.Collections.Generic;
using System.Linq;
using Utils;
using System.Text.Json;

namespace Aoc21;

public static class Day18
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

    class Pair
    {
        public dynamic Left;
        public dynamic Right;
        public Pair? Parent;

        class PairCtorException: Exception
        {
            public PairCtorException(string err): base(err) {}
        }

        public Pair(dynamic left, dynamic right)
        {
            if (left is not Pair && left is not int) throw new PairCtorException("Left child is not valid");
            if (right is not Pair && right is not int) throw new PairCtorException("Right child is not valid");

            Left = left;
            if (Left is Pair) Left.Parent = this;

            Right = right;
            if (Right is Pair) Right.Parent = this;
        }

        public static Pair FromArray(dynamic arr)
        {
            JsonElement l = arr[0];
            JsonElement r = arr[1];

            try
            {
                l.TryGetInt32(out var li);

                try
                {
                    r.TryGetInt32(out var ri);
                    return new Pair(li, ri);
                } catch (PairCtorException e)
                {
                    throw e;
                } catch
                {
                    return new Pair(li, FromArray(r));
                }
            } catch (PairCtorException e)
            {
                throw e;
            } catch
            {
                try
                {
                    r.TryGetInt32(out var ri);
                    return new Pair(FromArray(l), ri);
                } catch (PairCtorException e)
                {
                    throw e;
                } catch
                {
                    return new Pair(FromArray(l), FromArray(r));
                }
            }
        }

        public Pair DeepClone()
        {
            if (Left is int && Right is int) return new Pair(Left, Right);
            else if (Left is int) return new Pair(Left, Right.DeepClone());
            else if (Right is int) return new Pair(Left.DeepClone(), Right);
            else return new Pair(Left.DeepClone(), Right.DeepClone());
        }

        public int Magnitude()
        {
            if (Left is int && Right is int) return 3*Left + 2*Right;
            else if (Left is int) return 3*Left + 2*Right.Magnitude();
            else if (Right is int) return 3*Left.Magnitude() + 2*Right;
            else return 3*Left.Magnitude() + 2*Right.Magnitude();
        }

        public bool Explode(int depth, bool isRight)
        {
            var changedL = false;
            var changedR = false;
            if (Left is int && Right is int)
            {
                if (depth >= 4)
                {
                    if (Parent == null) return false; // Should never happen
                    if (isRight)
                    {
                        if (Parent.Left is int)
                        {
                            Parent.Left += Left;
                        } else
                        {
                            var cur = Parent.Left;
                            while (cur.Right is not int)
                            {
                                cur = cur.Right;
                            }
                            cur.Right += Left;
                        }

                        var prev = this;
                        var curParent = Parent;
                        while (curParent != null && curParent.Right is not int && curParent.Right == prev)
                        {
                            prev = curParent;
                            curParent = curParent!.Parent;
                        }
                        if (curParent == null)
                        {
                            // No Rightmost element
                            Parent.Right = 0;
                        } else if (curParent.Right is int)
                        {
                            curParent.Right += Right;
                        } else
                        {
                            var cur = curParent.Right;
                            while (cur.Left is not int)
                            {
                                cur = cur.Left;
                            }
                            cur.Left += Right;
                        }
                        Parent.Right = 0;
                    } else
                    {
                        if (Parent.Right is int)
                        {
                            Parent.Right += Right;
                        } else
                        {
                            var cur = Parent.Right;
                            while (cur.Left is not int)
                            {
                                cur = cur.Left;
                            }
                            cur.Left += Right;
                        }

                        var prev = this;
                        var curParent = Parent;
                        while (curParent != null && curParent.Left is not int && curParent.Left == prev)
                        {
                            prev = curParent;
                            curParent = curParent!.Parent;
                        }
                        if (curParent == null)
                        {
                            // No Leftmost element
                            Parent.Left = 0;
                        } else if (curParent.Left is int)
                        {
                            curParent.Left += Left;
                        } else
                        {
                            var cur = curParent.Left;
                            while (cur.Right is not int)
                            {
                                cur = cur.Right;
                            }
                            cur.Right += Left;
                        }
                        Parent.Left = 0;
                    }
                    return true;
                }
            } else if (Right is int)
            {
                changedL = Left.Explode(depth + 1, false);
            } else if (Left is int)
            {
                changedR = Right.Explode(depth + 1, true);
            } else
            {
                changedL = Left.Explode(depth + 1, false);
                if (!changedL)
                    changedR = Right.Explode(depth + 1, true);
            }
            return changedL || changedR;
        }

        public (dynamic, bool) Split()
        {
            var changedL = false;
            var changedR = false;
            if (Left is int && Right is int)
            {
                if (Left >= 10)
                {
                    Left = new Pair(Left/2, Left/2 + Left%2);
                    Left.Parent = this;
                    changedL = true;
                }
                if (!changedL && Right >= 10)
                {
                    Right = new Pair(Right/2, Right/2 + Right%2);
                    Right.Parent = this;
                    changedR = true;
                }
            } else if (Left is int)
            {
                if (Left >= 10)
                {
                    Left = new Pair(Left/2, Left/2 + Left%2);
                    Left.Parent = this;
                    changedL = true;
                } else
                {
                    var result = Right.Split();
                    Right = result.Item1;
                    changedR = result.Item2;
                }
            } else if (Right is int)
            {
                var result = Left.Split();
                Left = result.Item1;
                changedL = result.Item2;
                if (!changedL && Right >= 10)
                {
                    Right = new Pair(Right/2, Right/2 + Right%2);
                    Right.Parent = this;
                    changedR = true;
                }
            } else
            {
                var resultL = Left.Split();
                Left = resultL.Item1;
                changedL = resultL.Item2;
                
                if (!changedL)
                {
                    var resultR = Right.Split();
                    Right = resultR.Item1;
                    changedR = resultR.Item2;
                }
                
            }
            return (this, changedL || changedR);
        }

        public override string ToString()
        {
            return $"[{Left},{Right}]";
        }
    }

    // 4184
    static string PartA()
    {
        var f = Utils.Utils.OpenInput("18");
        var pairs = f
            .Select<string, Pair>((s) => Pair.FromArray(JsonSerializer.Deserialize<dynamic>(s)))
            .ToArray();
        foreach (var p in pairs) p.Parent = null;
        while (pairs.Length > 1)
        {
            pairs = new Pair[] {new Pair(pairs[0], pairs[1])}.Concat(pairs.Skip(2)).ToArray();
            var modified = true;
            while (modified)
            {
                modified = false;
                var resultE = pairs[0].Explode(0, false);
                modified |= resultE;
                
                if (resultE) continue;

                var resultS = pairs[0].Split();
                pairs[0] = resultS.Item1;
                modified |= resultS.Item2;
                if (resultS.Item2) continue;
            }
        }
        return pairs[0].Magnitude().ToString();
    }

    // 4731 
    static string PartB()
    {
        var f = Utils.Utils.OpenInput("18");
        var pairs = f
            .Select<string, Pair>((s) => Pair.FromArray(JsonSerializer.Deserialize<dynamic>(s)))
            .ToArray();
        foreach (var p in pairs) p.Parent = null;
        int maxMagnitude = int.MinValue;
        for (int i = 0; i < pairs.Length; i++)
        {
            for (int j = 0; j < pairs.Length; j++)
            {
                var p = new Pair(pairs[i].DeepClone(), pairs[j].DeepClone());
                var modified = true;
                while (modified)
                {
                    modified = false;
                    var resultE = p.Explode(0, false);
                    modified |= resultE;
                    
                    if (resultE) continue;

                    var resultS = p.Split();
                    p = resultS.Item1;
                    modified |= resultS.Item2;
                    if (resultS.Item2) continue;
                }
                maxMagnitude = Math.Max(maxMagnitude, p.Magnitude());
            }
        }
        return maxMagnitude.ToString();
    }
}