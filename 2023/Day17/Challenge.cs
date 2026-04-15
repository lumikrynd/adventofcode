using Helpers;
using Helpers.AStarFun;
using NUnit.Framework;
using Y2023.Day17.Models;

namespace Y2023.Day17;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day17/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Day17/Example2.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("102"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("94"));
	}

	[Test]
	public void Part2_Example_2()
	{
		var challenge = new Challenge(ExampleInput2);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("71"));
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var map = Parser.Parse(input);
		int width = map.GetLength(0);
		int height = map.GetLength(1);

		var gameMap = new GameMap(map, new(0, 0), new(width - 1, height - 1));
		return CalculateHeatLoss(map, gameMap).ToString();
	}

	public string Part2()
	{
		var map = Parser.Parse(input);
		int width = map.GetLength(0);
		int height = map.GetLength(1);

		var gameMap = new UltraGameMap(map, new(0, 0), new(width - 1, height - 1));
		return CalculateHeatLoss(map, gameMap).ToString();
	}

	private static long CalculateHeatLoss(int[,] map, GameMap gameMap)
	{
		var searcher = new AStar<GameMap.Node>(gameMap);
		searcher.Search();
		var foundPath = searcher.GetPath();

		var heatLoss = foundPath
			.Skip(1)
			.Select(n => n.Coordinate)
			.Select(c => map[c.X, c.Y])
			.Sum();

		return heatLoss;
	}
}