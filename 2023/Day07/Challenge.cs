using NUnit.Framework;

namespace Y2023.Day07;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(6440));
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
		Assert.That(result, Is.EqualTo(5905));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
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
		return result;
	}

	private long Part2(IEnumerable<string> input)
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
		return result;
	}
}

