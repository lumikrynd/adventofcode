using Helpers;
using Y2023.Day03.Models;

namespace Y2023.Day03;

public class Challenge(params IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var map = Parser.ParseMap(input);

		int value = 0;
		foreach(var part in map.Parts)
		{
			var adjacent = part.Coordinates
				.SelectMany(Adjacent);

			if(adjacent.Any(map.Symbols.Keys.Contains))
				value += part.Value;
		}

		return value.ToString();
	}

	public string Part2()
	{
		var map = Parser.ParseMap(input);
		var starMap = CreateMapOverStars(map);
		long value = CalculateGearing(starMap);

		return value.ToString();
	}

	private static Dictionary<Coordinate, List<int>> CreateMapOverStars(Map map)
	{
		var starMap = new Dictionary<Coordinate, List<int>>();

		foreach(var part in map.Parts)
		{
			var adjacentStar = part.Coordinates
				.SelectMany(Adjacent)
				.Distinct()
				.Where(c => map.Symbols.GetValueOrDefault(c) == '*');

			foreach(var possibleGear in adjacentStar)
			{
				AddStarToMap(possibleGear, part.Value);
			}
		}

		return starMap;

		void AddStarToMap(Coordinate coord, int value)
		{
			if(!starMap!.TryGetValue(coord, out var existing))
				starMap[coord] = existing = new();

			existing.Add(value);
		}
	}

	private static long CalculateGearing(Dictionary<Coordinate, List<int>> starMap)
	{
		long value = 0;
		foreach(var item in starMap)
		{
			if(item.Value.Count != 2)
				continue;

			value += (long)item.Value[0] * item.Value[1];
		}

		return value;
	}

	public static IEnumerable<Coordinate> Adjacent(Coordinate coord)
	{
		yield return coord.AddVector((-1, -1));
		yield return coord.AddVector((-1, 0));
		yield return coord.AddVector((-1, 1));
		yield return coord.AddVector((0, -1));
		yield return coord.AddVector((0, 1));
		yield return coord.AddVector((1, -1));
		yield return coord.AddVector((1, 0));
		yield return coord.AddVector((1, 1));
	}
}