using Y2023.Day04.Models;

namespace Y2023.Day04;

public class Parser
{
	public static List<Card> ParseCards(IEnumerable<string> lines)
	{
		var parser = new Parser();
		parser.Parse(lines);
		return parser.Result();
	}

	List<Card> Cards = new();

	private Parser()
	{
	}

	public List<Card> Result() => Cards;

	private static readonly char[] splitOn = { ':', '|' };
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public void Parse(IEnumerable<string> lines)
	{
		foreach (var line in lines)
		{
			var parts = line.Split(splitOn, splitOptions);
			var id = ParseId(parts[0]);
			var winners = ParseNumbers(parts[1]);
			var numbers = ParseNumbers(parts[2]);

			var card = new Card(id, winners, numbers);
			Cards.Add(card);
		}
	}

	public int ParseId(string idString)
	{
		var parts = idString.Split(' ', splitOptions);
		return int.Parse(parts[1]);
	}

	public List<int> ParseNumbers(string numberString)
	{
		var parts = numberString.Split(' ', splitOptions);
		var numbers = parts
			.Select(int.Parse)
			.ToList();
		return numbers;
	}
}