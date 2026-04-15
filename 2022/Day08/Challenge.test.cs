using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day08;

public class ChallengeTest
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