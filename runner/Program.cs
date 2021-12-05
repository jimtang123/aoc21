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
            if (!int.TryParse(args[0], out var _))
            {
                Console.WriteLine($"Could not parse day value ({args[0]})");
                return;
            }
            if (args[1] != "a" && args[1] != "b")
            {
                Console.WriteLine($"Could not parse problem value ({args[1]})");
                return;
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