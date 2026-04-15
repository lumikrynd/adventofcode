using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day08;

public class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day08/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1_V1();
		result.Should().Be("21");
	}

	[Test]
	public void Part1_V2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1_V2();
		result.Should().Be("21");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be("8");
	}
}

public class Challenge(IEnumerable<string> puzzleInput) : ISolver
{
	public string Part1() => Part1_V1();

	public string Part1_V1()
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);

		var count = groove.CountVisibleTrees();

		return count.ToString();
	}

	public string Part1_V2()
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);

		var count = groove.CountVisibleTreesV2();

		return count.ToString();
	}

	public string Part2()
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);
		(int value, var coordinate) = ScenicSpotFinder.FindScenicSpotValue(groove);
		return value.ToString();
	}
}