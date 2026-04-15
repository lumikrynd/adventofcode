using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day15;

internal class ChallengeTest
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day15/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1(10);
		result.Should().Be(26);
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2(0, 20);
		result.Should().Be(56000011);
	}
}