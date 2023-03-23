using System.Diagnostics;
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
		result.Should().Be(4873353);
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
		var timer = new Stopwatch();
		timer.Start();

		var result = Part2(PuzzleInput, 0, 4000000);
		result.Should().Be(11600823139120);

		timer.Stop();
		Console.WriteLine($"Time taken: {timer.Elapsed.TotalSeconds}");
	}

	private long Part2(IEnumerable<string> input, int min, int max)
	{
		var sensorResponses = SensorResponseParser.ParseInput(input);
		var map = MapCreater.CreateMap(sensorResponses);

		if(!map.TryGetUncoveredSpot(new(min, min), new(max, max), out var coordinate))
			throw new NotImplementedException();

		Console.WriteLine($"Found spot: {coordinate}");

		long frequency = (long)coordinate.X * 4000000 + coordinate.Y;
		Console.WriteLine($"Frequency: {frequency}");
		return frequency;
	}

	[Test]
	public void PerformanceTest()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		BuildSensorWithFarthestReach();
		stopwatch.Stop();
		Console.WriteLine($"Seconds: {stopwatch.Elapsed.TotalSeconds}");
	}

	private void BuildSensorWithFarthestReach()
	{
		var sensorResponses = SensorResponseParser.ParseInput(PuzzleInput);
		var maxSensor = sensorResponses.MaxBy(s => s.Sensor.ManhattenDistance(s.Beacon)) ?? throw new Exception();
		var single = new List<SensorResponse> { maxSensor };
		var map = MapCreater.CreateMap(single);
	}
}
