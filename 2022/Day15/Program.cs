using Day15.Model;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Day15;

internal class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput, 10);
		result.Should().Be(26);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput, 2000000);
		//result.Should().Be(???);
	}

	private int Part1(IEnumerable<string> input, int row)
	{
		throw new NotImplementedException();
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		//result.Should().Be(???);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		var result = Part2(PuzzleInput);
		//result.Should().Be(???);
	}

	private int Part2(IEnumerable<string> textMap)
	{
		throw new NotImplementedException();
	}
}
