using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Day06;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	string PuzzleInput => File.ReadAllText(@"Input/Puzzle.txt");

	public static IEnumerable<object[]> ExampleData()
	{
		yield return new object[] { "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7, 19 };
		yield return new object[] { "bvwbjplbgvbhsrlpgdmjqwftvncz", 5, 23 };
		yield return new object[] { "nppdvjthqldpwncqszvftbrmjlhg", 6, 23 };
		yield return new object[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10, 29 };
		yield return new object[] { "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11, 26 };
	}

	[TestCaseSource(nameof(ExampleData))]
	public void Part1_Example(string input, int expectedResult, int _)
	{
		var result = Part1(input);
		result.Should().Be(expectedResult);
	}

	[Test]
	public void Part1_Puzzle()
	{
		Part1(PuzzleInput);
	}

	public int Part1(string input)
	{
		return PuzzleSolution(input, 4);
	}

	[TestCaseSource(nameof(ExampleData))]
	public void Part2_Example(string input, int _, int expectedResult)
	{
		var result = Part2(input);
		result.Should().Be(expectedResult);
	}

	[Test]
	public void Part2_Puzzle()
	{
		Part2(PuzzleInput);
	}

	public int Part2(string input)
	{
		return PuzzleSolution(input, 14);
	}

	private static int PuzzleSolution(string input, int packageSize)
	{
		CircularStorage storage = new(packageSize);
		int count = packageSize - 1;
		foreach(char c in input.Take(packageSize - 1))
		{
			storage.AddValue(c);
		}

		foreach(char c in input.Skip(packageSize - 1))
		{
			count++;
			storage.AddValue(c);
			if(storage.Distinct().Count() == packageSize)
				break;
		}

		Console.WriteLine($"Result: {count}");
		return count;
	}
}