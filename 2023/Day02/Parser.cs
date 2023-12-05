using Y2023.Day02.Models;

namespace Y2023.Day02;

internal class Parser
{
	public static List<Game> ParseGames(IEnumerable<string> input)
	{
		return input
			.Select(ParseGame)
			.ToList();
	}

	private static Game ParseGame(string input)
	{
		var parts = input.Split(": ");
		int id = ParseId(parts[0]);
		List<Set> sets = ParseSets(parts[1]);

		return new Game()
		{
			GameId = id,
			Sets = sets,
		};
	}

	private static int ParseId(string input)
	{
		var parts = input.Split(" ");
		return int.Parse(parts[1]);
	}

	private static List<Set> ParseSets(string input)
	{
		var parts = input.Split("; ");
		return parts
			.Select(ParseSet)
			.ToList();
	}

	private static Set ParseSet(string input)
	{
		var parts = input.Split(", ");

		return new Set()
		{
			ColorCounts = parts.Select(ParseCount).ToList(),
		};
	}

	private static (Color, int) ParseCount(string input)
	{
		var parts = input.Split(" ");
		var count = int.Parse(parts[0]);
		var color = Enum.Parse<Color>(parts[1], true);
		return (color, count);
	}
}
