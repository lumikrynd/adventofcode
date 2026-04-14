using Helpers;
using NUnit.Framework;

namespace Y2023.Day07;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("6440"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("5905"));
	}
}

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

		Console.Write($"Result: {result}");
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

		Console.Write($"Result: {result}");
		return result.ToString();
	}
}
