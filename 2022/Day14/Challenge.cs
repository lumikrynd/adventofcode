using Y2022.Day14.Model;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day14;

internal class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be("24");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be("93");
	}
}

public class Challenge(IEnumerable<string> textMap) : ISolver
{
	public string Part1()
	{
		var sandSource = new Coordinate(500, 0);
		var caveSpecification = CaveSpecificationParser.Parse(textMap);
		var cave = new CaveMap(caveSpecification);
		var SandSimulator = new SandSimulator(cave);
		SandSimulator.FillSandFrom(sandSource);

		return SandSimulator.SandFilled.ToString();
	}

	public string Part2()
	{
		var sandSource = new Coordinate(500, 0);
		var caveSpecification = CaveSpecificationParser.Parse(textMap);
		var cave = new CaveMapWithFloor(caveSpecification);
		var SandSimulator = new SandSimulator(cave);
		SandSimulator.FillSandFrom(sandSource);

		return SandSimulator.SandFilled.ToString();
	}
}