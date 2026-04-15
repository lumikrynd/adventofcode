using Helpers;

namespace Y2022.Day02;

public class Challenge : ISolver
{
	private readonly string input;

	public Challenge(string input)
	{
		this.input = input;
	}

	public Challenge(IEnumerable<string> lines)
	{
		input = string.Join('\n', lines);
	}

	public string Part1()
	{
		var games = Parser.ParseInput(input);

		var totalScore = games.Sum(g => g.Score());

		return totalScore.ToString();
	}

	public string Part2()
	{
		var games = Parser.ParseInputV2(input);

		var totalScore = games.Sum(g => g.Score());

		return totalScore.ToString();
	}
}