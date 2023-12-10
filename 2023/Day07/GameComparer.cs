using System.Diagnostics.CodeAnalysis;
using Y2023.Day07.Models;

namespace Y2023.Day07;

public class GameComparer : IComparer<Game>
{
	public int Compare(Game? x, Game? y)
	{
		int result;
		if(HandledNull(x, y, out result))
			return result;

		result = CompareType(x, y);
		if(result != 0)
			return result;

		return CompareCards(x, y);
	}

	private bool HandledNull(
		[NotNullWhen(false)] Game? x,
		[NotNullWhen(false)] Game? y,
		out int result)
	{
		result = 0;

		if(x is null)
			result = -1;
		else if(y is null)
			result = 1;

		return x is null || y is null;
	}

	private int CompareCards(Game x, Game y)
	{
		var cardPairs = x.Hand.Zip(y.Hand);
		foreach((char xCard, char yCard) in cardPairs)
		{
			if(xCard != yCard)
				return CardToValue(xCard) - CardToValue(yCard);
		}
		return 0;
	}

	private int CompareType(Game x, Game y)
	{
		var xTypeValue = ToHandTypeValue(x.Hand);
		var yTypeValue = ToHandTypeValue(y.Hand);

		return xTypeValue - yTypeValue;
	}

	private int ToHandTypeValue(char[] hand)
	{
		var handType = SameKindCount(hand);
		return HandTypeToValue(handType);
	}

	private int HandTypeToValue(int[] handType)
	{
		return handType switch
		{
			[5, ..] => 6,
			[4, ..] => 5,
			[3, 2, ..] => 4,
			[3, ..] => 3,
			[2, 2, ..] => 2,
			[2, ..] => 1,
			_ => 0,
		};
	}

	protected virtual int[] SameKindCount(char[] hand)
	{
		return hand
		   .GroupBy(x => x)
		   .Select(x => x.Count())
		   .OrderDescending()
		   .ToArray();
	}

	protected virtual int CardToValue(char c) => c switch
	{
		'T' => 10,
		'J' => 11,
		'Q' => 12,
		'K' => 13,
		'A' => 14,
		var n when n >= '2' && n <= '9' => n - '0',
		_ => throw new NotImplementedException(),
	};
}

