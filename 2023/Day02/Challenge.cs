using NUnit.Framework;
using Y2023.Day02.Models;

namespace Y2023.Day02;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(8));
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
		Assert.That(result, Is.EqualTo(2286));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private int Part1(IEnumerable<string> exampleInput)
	{
		var games = Parser.ParseGames(exampleInput);
		var result = games
			.Where(IsPossible)
			.Sum(x => x.GameId);

		Console.WriteLine($"Result: {result}");
		return result;
	}

	private static Set CubeLimit = new()
	{
		ColorCounts = new()
		{
			(Color.Red , 12),
			(Color.Green , 13),
			(Color.Blue , 14),
		}
	};

	private bool IsPossible(Game game)
	{
		return game.Sets.All(CubeLimit.IsSupersetOff);
	}

	private int Part2(IEnumerable<string> exampleInput)
	{
		var games = Parser.ParseGames(exampleInput);

		var result = games
			.Select(ToNeededColors)
			.Select(SetPower)
			.Sum();

		Console.WriteLine($"Result: {result}");
		return result;
	}

	private Set ToNeededColors(Game game)
	{
		var maxColors = game.Sets
			.SelectMany(s => s.ColorCounts)
			.GroupBy(x => x.Color)
			.Select(g => (Color: g.Key, Count: g.Max(i => i.Count)))
			.ToList();

		return new Set()
		{
			ColorCounts = maxColors,
		};
	}

	private int SetPower(Set set)
	{
		if(set.ColorCounts.Count != 3)
			return 0;

		return set.ColorCounts
			.Select(x => x.Count)
			.Aggregate((a, b) => a * b);
	}
}