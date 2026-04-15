using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day04;

public class ChallengeTest
{
	static IEnumerable<string> ExampleData => File.ReadLines(@"Input/Day04/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var c = new Challenge(ExampleData);
		c.Part1().Should().Be("2");
	}

	[Test]
	public void Part2_Example()
	{
		var c = new Challenge(ExampleData);
		c.Part2().Should().Be("4");
	}
}