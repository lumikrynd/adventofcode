using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day11;

public class ChallengeTest
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day11/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be("10605");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be("2713310158");
	}
}