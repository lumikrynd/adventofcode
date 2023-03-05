using Day14.Model;
using Helpers;

namespace Day14;

internal class CaveSpecificationParser
{
	public static List<RockFormation> Parse(IEnumerable<string> input)
	{
		var cave = new List<RockFormation>();
		foreach(var line in input)
		{
			var rockFormation = ParseRockFormation(line);
			cave.Add(rockFormation);
		}
		return cave;
	}

	private static RockFormation ParseRockFormation(string input)
	{
		var rockFormation = new RockFormation();
		var coordinates = input.Split(" -> ");
		foreach(var coordinate in coordinates)
		{
			var parsedCoordinate = ParseCoordinate(coordinate);
			rockFormation.Add(parsedCoordinate);
		}
		return rockFormation;
	}

	private static Coordinate ParseCoordinate(string input)
	{
		var parts = input.Split(',');
		var x = int.Parse(parts[0]);
		var y = int.Parse(parts[1]);
		return new(x, y);
	}
}
