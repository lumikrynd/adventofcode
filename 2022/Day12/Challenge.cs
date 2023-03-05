using Day12.Maps;
using Day12.Pathfinding;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Day12;
internal class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");


	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(31);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput);
		result.Should().Be(370);
	}

	private int Part1(IEnumerable<string> textMap)
	{
		var map = MapParser.ParseMap(textMap.ToArray());
		map.Start = map.S;
		map.FoundEnd = coord => coord.Equals(map.E);
		map.StepPossible = NormalTraverselStepChecker(map);

		var path = BreathFirstSearch.CalculatePath(map);
		Console.WriteLine(path.Count);
		return path.Count;
	}

	private Func<Coordinate, Coordinate, bool> NormalTraverselStepChecker(Map map)
	{
		return (from, to) =>
		{
			var maxHeigh = map.GetHeightAt(from) + 1;
			return map.GetHeightAt(to) <= maxHeigh;
		};
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(29);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		var result = Part2(PuzzleInput);
		result.Should().Be(363);
	}

	private int Part2(IEnumerable<string> textMap)
	{
		var map = MapParser.ParseMap(textMap.ToArray());
		map.Start = map.E;
		map.FoundEnd = coord => map.GetHeightAt(coord) <= 0;
		map.StepPossible = InvertedTraverselStepChecker(map);

		var path = BreathFirstSearch.CalculatePath(map);
		Console.WriteLine(path.Count);
		return path.Count;
	}

	private Func<Coordinate, Coordinate, bool> InvertedTraverselStepChecker(Map map)
	{
		var temp = NormalTraverselStepChecker(map);
		return (from, to) => temp(to, from);
	}
}