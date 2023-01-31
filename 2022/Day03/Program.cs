using System;
using System.Collections.Generic;
using System.IO;

namespace Day03;

internal class Program
{
	static void Main(string[] args)
	{
		var example = File.ReadLines(@"Data/Example.txt");
		var mainPuzzle = File.ReadLines(@"Data/Puzzle.txt");

		Console.WriteLine(@"Part 1");
		Part1(example);
		Part1(mainPuzzle);
		Console.WriteLine();

		Console.WriteLine(@"Part 2");
		Part2(example);
		Part2(mainPuzzle);
	}

	private static void Part1(IEnumerable<string> input)
	{
		int prioritySum = 0;

		var backpacks = BackpackParser.ParseBackpacks(input);
		foreach(var backpack in backpacks)
		{
			prioritySum += backpack.GetRearrangementPriority();
		}

		Console.WriteLine($"PrioritySum: {prioritySum}");
	}

	private static void Part2(IEnumerable<string> input)
	{
		int prioritySum = 0;

		var groups = BackpackerGroupParser.ParseBackpackerGroups(input);
		foreach(var group in groups)
		{
			prioritySum += group.GetRearrangementPriority();
		}

		Console.WriteLine($"PrioritySum: {prioritySum}");
	}
}