using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day07;

public class ChallengeTest
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day07/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be(95437.ToString());
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be(24933642.ToString());
	}
}