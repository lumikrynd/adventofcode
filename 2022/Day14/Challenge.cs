using Day14.Model;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Day14;

internal class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(24);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput);
		result.Should().Be(719);
	}

	private int Part1(IEnumerable<string> textMap)
	{
		var sandSource = new Coordinate(500, 0);
		var caveSpecification = CaveSpecificationParser.Parse(textMap);
		var cave = new CaveMap(caveSpecification);
		var SandSimulator = new SandSimulator(cave);
		SandSimulator.FillSandFrom(sandSource);

		Console.WriteLine($"Sand filled: {SandSimulator.SandFilled}");
		return SandSimulator.SandFilled;
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(93);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		var result = Part2(PuzzleInput);
		result.Should().Be(23390);
	}

	private int Part2(IEnumerable<string> textMap)
	{
		var sandSource = new Coordinate(500, 0);
		var caveSpecification = CaveSpecificationParser.Parse(textMap);
		var cave = new CaveMapWithFloor(caveSpecification);
		var SandSimulator = new SandSimulator(cave);
		SandSimulator.FillSandFrom(sandSource);

		Console.WriteLine($"Sand filled: {SandSimulator.SandFilled}");
		return SandSimulator.SandFilled;
	}
}