using Helpers;

namespace Y2023.Day07;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var games = Parser.Parse(input);

		var weakestFirst = games
			.Order(new GameComparer())
			.ToArray();

		long result = 0;
		for(int i = 0; i < weakestFirst.Length; i++)
		{
			result += (i + 1) * weakestFirst[i].Bid;
		}

		return result.ToString();
	}

	public string Part2()
	{
		var games = Parser.Parse(input);

		var weakestFirst = games
			.Order(new GameJokerComparer())
			.ToArray();

		long result = 0;
		for(int i = 0; i < weakestFirst.Length; i++)
		{
			result += (i + 1) * weakestFirst[i].Bid;
		}

		return result.ToString();
	}
}