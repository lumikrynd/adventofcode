using System;
using System.Linq;

namespace Day1;

internal partial class Program
{
	static void Main(string[] args)
	{
		Part1(Example);

		Console.WriteLine();
		Console.WriteLine();

		Part1(Input);

		Console.WriteLine();
		Console.WriteLine();

		Part2(Example);

		Console.WriteLine();
		Console.WriteLine();

		Part2(Input);
	}

	private static void Part1(string input)
	{
		var elfs = Parser.ParseInput(input);

		var maxTotal = elfs.Max(s => s.Sum());

		Console.Write($"Sum: {maxTotal}");
	}

	private static void Part2(string input)
	{
		var elfs = Parser.ParseInput(input);

		var ordered = elfs.OrderByDescending(elf => elf.Sum());

		var sum = ordered.Take(3).Sum(elf => elf.Sum());

		Console.Write($"Sum: {sum}");
	}
}
