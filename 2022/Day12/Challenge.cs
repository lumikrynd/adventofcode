using Y2022.Day12.Maps;
using Y2022.Day12.Pathfinding;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day12;

internal class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be("31");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be("29");
	}
}

public class Challenge(IEnumerable<string> textMap) : ISolver
{
	public string Part1()
	{
		var map = MapParser.ParseMap(textMap.ToArray());
		map.Start = map.S;
		map.FoundEnd = coord => coord.Equals(map.E);
		map.StepPossible = NormalTraverselStepChecker(map);

		var path = BreathFirstSearch.CalculatePath(map);
		return path.Count.ToString();
	}

	private Func<Coordinate, Coordinate, bool> NormalTraverselStepChecker(Map map)
	{
		return (from, to) =>
		{
			var maxHeigh = map.GetHeightAt(from) + 1;
			return map.GetHeightAt(to) <= maxHeigh;
		};
	}

	public string Part2()
	{
		var map = MapParser.ParseMap(textMap.ToArray());
		map.Start = map.E;
		map.FoundEnd = coord => map.GetHeightAt(coord) <= 0;
		map.StepPossible = InvertedTraverselStepChecker(map);

		var path = BreathFirstSearch.CalculatePath(map);
		return path.Count.ToString();
	}

	private Func<Coordinate, Coordinate, bool> InvertedTraverselStepChecker(Map map)
	{
		var temp = NormalTraverselStepChecker(map);
		return (from, to) => temp(to, from);
	}
}