using Day12.Maps;
using Day12.Pathfinding;
using FluentAssertions;
using NUnit.Framework;

namespace Day12;
internal partial class Challenge
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
		var path = BreathFirstSearch.CalculatePath(map);
		Console.WriteLine(path.Count);
		return path.Count;
	}
}