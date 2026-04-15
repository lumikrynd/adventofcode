using Y2022.Day15.Parsing;
using Helpers;

namespace Y2022.Day15;

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