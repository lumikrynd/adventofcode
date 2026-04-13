using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day04;

public class Tests
{
	static IEnumerable<string> ExampleData => File.ReadLines(@"Data/Example.txt");

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

public class Challenge(IEnumerable<string> data) : ISolver
{
	public string Part1()
	{
		var groups = AssignmentGroupParser.Parse(data);

		var containedCount = groups.Count(g => g.OneContainsAll());
		return $"{containedCount}";
	}

	public string Part2()
	{
		var groups = AssignmentGroupParser.Parse(data);

		var overlapCount = groups.Count(g => g.HasOverlaps());
		return $"{overlapCount}";
	}
}