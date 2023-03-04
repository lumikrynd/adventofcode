using Day11.Models;
using Day11.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Day11;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(10605);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput);
		result.Should().Be(78960);
	}

	private int Part1(IEnumerable<string> puzzleInput)
	{
		var parser = MonkeyListParser.CreateParser();
		var monkeys = parser.Parse(puzzleInput.ToList());
		for(int i = 0; i < 20; i++)
		{
			foreach(var monkey in monkeys)
			{
				monkey.InspectItems();
				monkey.PostInspectRelief();
				monkey.ThrowItems(monkeys);
			}
		}

		for(int i = 0; i < monkeys.Count; i++)
		{
			var monkey = monkeys[i];
			Console.WriteLine($"Monkey {i}: {monkey.InspectCount}");
		}

		var SortedInspectCounts = monkeys
			.Select(x => x.InspectCount)
			.OrderByDescending(x => x)
			.ToList();

		var result = SortedInspectCounts[0] * SortedInspectCounts[1];
		Console.WriteLine($"Multiply result: {result}");
		return result;
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(2713310158);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		var result = Part2(PuzzleInput);
		result.Should().Be(14561971968);
	}

	private long Part2(IEnumerable<string> puzzleInput)
	{
		var parser = MonkeyListParser.CreateParser();
		var monkeys = parser.Parse(puzzleInput.ToList());
		var leastCommonMultiple = monkeys
			.Select(x => x.DivisorTest)
			.Aggregate(1, LeastCommonMultiple);

		for(int i = 1; i <= 10000; i++)
		{
			foreach(var monkey in monkeys)
			{
				monkey.InspectItems();
				monkey.ReduceWorryByDivisor(leastCommonMultiple);
				monkey.ThrowItems(monkeys);
			}
			LogMonkeys(i, monkeys);
		}

		for(int i = 0; i < monkeys.Count; i++)
		{
			var monkey = monkeys[i];
			Console.WriteLine($"Monkey {i}: {monkey.InspectCount}");
		}

		var SortedInspectCounts = monkeys
			.Select(x => x.InspectCount)
			.OrderByDescending(x => x)
			.ToList();

		var result = (long)SortedInspectCounts[0] * SortedInspectCounts[1];
		Console.WriteLine($"Multiply result: {result}");
		return result;
	}

	private void LogMonkeys(int round, List<Monkey> monkeys)
	{
		if(round is 1 or 20 || round % 1000 == 0)
		{
			Console.WriteLine($"Round: {round}");
			for(int i = 0; i < monkeys.Count; i++)
			{
				var monkey = monkeys[i];
				Console.WriteLine($"Monkey {i}: {monkey.InspectCount}");
			}
			Console.WriteLine();
		}
	}

	static int LeastCommonMultiple(int a, int b)
	{
		return (a / GreatestCommonFactor(a, b)) * b;
	}

	static int GreatestCommonFactor(int a, int b)
	{
		while (b != 0)
		{
			int temp = b;
			b = a % b;
			a = temp;
		}
		return a;
	}
}
