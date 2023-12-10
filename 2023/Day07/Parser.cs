using Y2023.Day07.Models;

namespace Y2023.Day07;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static List<Game> Parse(IEnumerable<string> lines) =>
		lines.Select(Parse).ToList();

	private static Game Parse(string line)
	{
		var parts = line.Split(' ', splitOptions);

		var bid = int.Parse(parts[1]);
		var hand = parts[0].ToArray();

		return new Game
		{
			Hand = hand,
			Bid = bid,
		};
	}
}