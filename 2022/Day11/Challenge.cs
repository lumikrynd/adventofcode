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
		Part1(PuzzleInput);
		//result.Should().Be(12640);
	}

	private int Part1(IEnumerable<string> puzzleInput)
	{
		var monkeys = MonkeyListParser.Parse(puzzleInput);
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
		Part2(ExampleInput);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private int Part2(IEnumerable<string> puzzleInput)
	{
		throw new NotImplementedException();
	}
}
