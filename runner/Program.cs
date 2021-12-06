#nullable enable
using Aoc21;
using System;
using System.Linq;
using System.Reflection;

namespace runner
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException($"Invalid number of arguments ({args.Length})");
            }
            if (!int.TryParse(args[0], out var _))
            {
                throw new ArgumentException($"Could not parse day value ({args[0]})");
            }
            if (args[1] != "a" && args[1] != "b")
            {
                throw new ArgumentException($"Could not parse problem part ({args[1]})");
            }
            
            var result = Assembly
                .Load(args[0])
                .GetType($"Aoc21.Day{args[0]}")!
                .GetMethod("Run")!
                .Invoke(null, new object[] {args[1]})!;

            var head = $"--- Day {args[0]:02d} ({args[1]}) ---";
            Console.WriteLine($"{head}\n{result.ToString()}\n{new String('-', head.Length)}");
        }
    }
}