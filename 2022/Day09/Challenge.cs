using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day09;

public class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Example2.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be("13");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be("1");
	}

	[Test]
	public void Part2_Example2()
	{
		var challenge = new Challenge(ExampleInput2);
		var result = challenge.Part2();
		result.Should().Be("36");
	}
}

public class Challenge(IEnumerable<string> puzzleInput) : ISolver
{
	public string Part1()
	{
		return CalculateWithRopeLength(2).ToString();
	}

	public string Part2()
	{
		return CalculateWithRopeLength(10).ToString();
	}

	private int CalculateWithRopeLength(int ropeLength)
	{
		var moves = MovesParser.Parse(puzzleInput);
		RopeEmulator re = new RopeEmulator(moves, ropeLength);
		int visitedCount = re.Visited.Count;
		return visitedCount;
	}
}