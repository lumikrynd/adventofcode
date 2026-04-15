using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day09;

public class ChallengeTest
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day09/Example.txt");
	IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Day09/Example2.txt");

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