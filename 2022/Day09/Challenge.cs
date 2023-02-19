using FluentAssertions;
using NUnit.Framework;

namespace Day09;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Example2.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(13);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput);
		result.Should().Be(6522);
	}

	private int Part1(IEnumerable<string> puzzleInput)
	{
		return CalculateWithRopeLength(puzzleInput, 2);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(1);
	}

	[Test]
	public void Part2_Example2()
	{
		var result = Part2(ExampleInput2);
		result.Should().Be(36);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private int Part2(IEnumerable<string> puzzleInput)
	{
		return CalculateWithRopeLength(puzzleInput, 10);
	}

	private static int CalculateWithRopeLength(IEnumerable<string> puzzleInput, int ropeLength)
	{
		var moves = MovesParser.Parse(puzzleInput);
		RopeEmulator re = new RopeEmulator(moves, ropeLength);
		int visitedCount = re.Visited.Count;
		Console.WriteLine($"visited: {visitedCount}");
		return visitedCount;
	}
}