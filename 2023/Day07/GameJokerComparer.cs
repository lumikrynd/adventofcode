using System.Diagnostics.CodeAnalysis;
using Y2023.Day07.Models;

namespace Y2023.Day07;

public class GameJokerComparer : GameComparer
{
	const char joker = 'J';
	protected override int[] SameKindCount(char[] hand)
	{
		var jokers = hand.Count(x => x == joker);

		var counts = hand
			.Where(x => x != joker)
			.GroupBy(x => x)
			.Select(x => x.Count())
			.Append(0)
			.OrderDescending()
			.ToArray();

		counts[0] += jokers;
		return counts;
	}

	protected override int CardToValue(char c) => c switch
	{
		joker => 0,
		var n => base.CardToValue(n),
	};
}

