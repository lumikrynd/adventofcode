using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day05;

public class ChallengeTest
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day05/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().BeEquivalentTo("CMZ");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().BeEquivalentTo("MCD");
	}
}