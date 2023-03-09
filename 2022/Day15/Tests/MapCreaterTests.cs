using Day15.Parsing;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Day15.Tests;

internal class MapCreaterTests
{
	public static IEnumerable<object[]> GetCoordinates()
	{
		yield return new[]
		{
			new List<Coordinate>(){new(0,0), new(23, 5), new(-3, 22), new(10, -33), new(-3, -3)},
		};
	}

	[TestCaseSource(nameof(GetCoordinates))]
	public void TestSingleSpots(IEnumerable<Coordinate> sensors)
	{
		var sensorResults = sensors
			.Select(x => new SensorResponse(x, x))
			.ToList();

		var map = MapCreater.CreateMap(sensorResults);

		foreach(var coordinate in sensors)
		{
			map.HasContent(coordinate).Should().BeTrue();
		}
	}

	[Test]
	public void DiagonalSensor_TestAllFields()
	{
		const int SIZE = 1 << 8;
		var sensorResults = Enumerable.Range(0, SIZE)
			.Select(x => new Coordinate(x, x))
			.Select(c => new SensorResponse(c, c))
			.ToList();

		var map = MapCreater.CreateMap(sensorResults);

		for(int x = 0; x < SIZE; x++)
		{
			for(int y = 0; y < SIZE; y++)
			{
				var coordinate = new Coordinate(x, y);
				map.HasContent(coordinate).Should().Be(x == y);
			}
		}
	}
}
