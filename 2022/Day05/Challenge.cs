using Y2022.Day05.Models;
using Y2022.Day05.Parsers;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day05;

public class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day05/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().BeEquivalentTo("CMZ");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().BeEquivalentTo("MCD");
	}
}

public class Challenge(IEnumerable<string> data) : ISolver
{
	public string Part1()
	{
		return SolvePuzzle(data, new OldCrane());
	}

	public string Part2()
	{
		return SolvePuzzle(data, new StackCrane());
	}

	private static string SolvePuzzle(IEnumerable<string> data, ICrane crane)
	{
		(var supplyStacks, var moves) = PuzzleParser.Parse(data);
		SupplyStacks boxArea = new SupplyStacks(crane, supplyStacks);
		foreach(var move in moves)
		{
			boxArea.Move(move);
		}

		var top = new List<char>();
		for(int i = 1; i <= boxArea.StackCount; i++)
		{
			var stackTop = boxArea.PeekStack(i);
			top.Add(stackTop);
		}
		var result = string.Concat(top);
		return result;
	}
}