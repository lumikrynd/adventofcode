using Helpers;
using NUnit.Framework;

namespace Y2022.Day02;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("15"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("12"));
	}
}

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