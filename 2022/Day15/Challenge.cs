using Y2022.Day15.Parsing;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day15;

internal class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day15/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1(10);
		result.Should().Be(26);
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2(0, 20);
		result.Should().Be(56000011);
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1() {
		return Part1(2_000_000).ToString();
	}

	public int Part1(int row)
	{
		var sensorResponses = SensorResponseParser.ParseInput(input);
		var map = MapCreater.CreateMap(sensorResponses);
		var coveredArea = map.CountRowCowerage(row);

		var beaconCount = sensorResponses
			.Select(s => s.Beacon)
			.Distinct()
			.Count(b => b.Y == row);

		var result = coveredArea - beaconCount;
		return result;
	}

	public string Part2() {
		return Part2(0, 4000000).ToString();
	}

	public long Part2(int min, int max)
	{
		var sensorResponses = SensorResponseParser.ParseInput(input);
		var map = MapCreater.CreateMap(sensorResponses);

		if(!map.TryGetUncoveredSpot(new(min, min), new(max, max), out var coordinate))
			throw new NotImplementedException();

		long frequency = (long)coordinate.X * 4000000 + coordinate.Y;
		return frequency;
	}
}