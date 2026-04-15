using Helpers;
using NUnit.Framework;

namespace Y2023.Day04;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day04/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("13"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("30"));
	}
}

public class Challenge(params IEnumerable<string> input) : ISolver
{
	public string Part1()
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

		return points.ToString();
	}

	public string Part2()
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

		return totalCards.ToString();
	}
}