using System;
using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;

namespace Day08;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(21);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	private int Part1(IEnumerable<string> puzzleInput)
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);

		var count = groove.CountVisibleTrees();
		Console.WriteLine($"Visible count: {count}");

		return count;
	}

	[Test]
	public void Part1_V2_Example()
	{
		var result = Part1_V2(ExampleInput);
		result.Should().Be(21);
	}

	[Test]
	public void Part1_V2_MainPuzzle()
	{
		Part1_V2(PuzzleInput);
	}

	private int Part1_V2(IEnumerable<string> puzzleInput)
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);

		var count = groove.CountVisibleTreesV2();
		Console.WriteLine($"Visible count: {count}");

		return count;
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(8);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private int Part2(IEnumerable<string> puzzleInput)
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);
		(int value, var coordinate) = ScenicSpotFinder.FindScenicSpotValue(groove);
		Console.WriteLine($"Scenic value: {value} at ({coordinate.X}, {coordinate.Y})");
		return value;
	}
}