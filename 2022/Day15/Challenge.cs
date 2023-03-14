using Day15.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Day15;

internal class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput, 10);
		result.Should().Be(26);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput, 2000000);
		//result.Should().Be(???);
	}

	private int Part1(IEnumerable<string> input, int row)
	{
		var sensorResponses = SensorResponseParser.ParseInput(input);
		var map = MapCreater.CreateMap(sensorResponses);
		var coveredArea = map.CountRowCowerage(row);
		Console.WriteLine($"Covered lines: {coveredArea}");

		var beaconCount = sensorResponses
			.Select(s => s.Beacon)
			.Distinct()
			.Count(b => b.Y == row);
		Console.WriteLine($"Beacons on line: {beaconCount}");

		var result = coveredArea - beaconCount;
		Console.WriteLine($"result: {result}");
		return result;
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput, 0, 20);
		result.Should().Be(56000011);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		var result = Part2(PuzzleInput, 0, 4000000);
		//result.Should().Be(???);
	}

	private long Part2(IEnumerable<string> textMap, int min, int max)
	{
		throw new NotImplementedException();
	}
}
