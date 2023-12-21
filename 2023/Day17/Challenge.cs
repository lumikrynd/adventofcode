using Helpers.AStarFun;
using NUnit.Framework;
using Y2023.Day17.Models;

namespace Y2023.Day17;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Example2.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(102));
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
		Assert.That(result, Is.EqualTo(94));
	}

	[Test]
	public void Part2_Example_2()
	{
		var result = Part2(ExampleInput2);
		Assert.That(result, Is.EqualTo(71));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var map = Parser.Parse(input);
		int width = map.GetLength(0);
		int height = map.GetLength(1);

		var gameMap = new GameMap(map, new(0, 0), new(width - 1, height - 1));
		return CalculateHeatLoss(map, gameMap);
	}

	private long Part2(IEnumerable<string> input)
	{
		var map = Parser.Parse(input);
		int width = map.GetLength(0);
		int height = map.GetLength(1);

		var gameMap = new UltraGameMap(map, new(0, 0), new(width - 1, height - 1));
		return CalculateHeatLoss(map, gameMap);
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

		Console.WriteLine($"Result: {heatLoss}");
		return heatLoss;
	}
}