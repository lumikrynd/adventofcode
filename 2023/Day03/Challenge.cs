using Helpers;
using NUnit.Framework;
using Y2023.Day03.Models;

namespace Y2023.Day03;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(4361));
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		Assert.That(result, Is.EqualTo(467835));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private int Part1(IEnumerable<string> input)
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

		Console.WriteLine($"Result: {value}");
		return value;
	}

	private long Part2(IEnumerable<string> input)
	{
		var map = Parser.ParseMap(input);
		var starMap = CreateMapOverStars(map);
		long value = CalculateGearing(starMap);

		Console.WriteLine($"Result: {value}");
		return value;
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