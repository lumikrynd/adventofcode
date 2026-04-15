using Helpers;
using NUnit.Framework;
using Y2023.Day02.Models;

namespace Y2023.Day02;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day02/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("8"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("2286"));
	}
}

public class Challenge(params IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var games = Parser.ParseGames(input);
		var result = games
			.Where(IsPossible)
			.Sum(x => x.GameId);

		return result.ToString();
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

	public string Part2()
	{
		var games = Parser.ParseGames(input);

		var result = games
			.Select(ToNeededColors)
			.Select(SetPower)
			.Sum();

		return result.ToString();
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