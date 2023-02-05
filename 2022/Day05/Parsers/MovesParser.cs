using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day05.Models;

namespace Day05.Parsers;

internal class MovesParser
{
	public static List<Move> Parse(IEnumerable<string> input)
	{
		return input.Select(ParseMove).ToList();
	}

	private static Move ParseMove(string input)
	{
		var parts = input.Split();
		int amount = int.Parse(parts[1]);
		int from = int.Parse(parts[3]);
		int to = int.Parse(parts[5]);
		return new Move(amount, from, to);
	}
}
