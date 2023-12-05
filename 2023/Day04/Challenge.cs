using NUnit.Framework;

namespace Y2023.Day04;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		int result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(13));
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
		Assert.That(result, Is.EqualTo(30));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private int Part1(IEnumerable<string> input)
	{
		var cards = Parser.ParseCards(input);

		int points = 0;
		foreach(var card in cards)
		{
			var winners = card.Winners.ToHashSet();
			var matches = card.Numbers.Count(winners.Contains);
			if(matches > 0)
				points += 1 << (matches - 1);
		}

		Console.WriteLine($"Total points: {points}");
		return points;
	}

	private int Part2(IEnumerable<string> input)
	{
		var cards = Parser.ParseCards(input);

		var cardCounts = Enumerable.Repeat(1, cards.Count + 1).ToArray();
		cardCounts[0] = 0;

		foreach(var card in cards)
		{
			var winners = card.Winners.ToHashSet();
			var matches = card.Numbers.Count(winners.Contains);

			int currentCount = cardCounts[card.Id];
			for(int i = card.Id + 1; i <= card.Id + matches && i < cardCounts.Length; i++)
			{
				cardCounts[i] += currentCount;
			}
		}

		var totalCards = cardCounts.Sum();

		Console.WriteLine($"Total cards: {totalCards}");
		return totalCards;
	}
}
