using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Versioning;
using Day05.Models;
using Day05.Parsers;
using FluentAssertions;
using NUnit.Framework;

namespace Day05;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().BeEquivalentTo("CMZ");
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().BeEquivalentTo("MCD");
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	public string Part1(IEnumerable<string> data)
	{
		return SolvePuzzle(data, new OldCrane());
	}

	public string Part2(IEnumerable<string> data)
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
		Console.WriteLine($"Result: {result}");
		return result;
	}
}