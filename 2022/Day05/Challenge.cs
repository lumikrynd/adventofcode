using Y2022.Day05.Models;
using Y2022.Day05.Parsers;
using Helpers;

namespace Y2022.Day05;

public class Challenge(IEnumerable<string> data) : ISolver
{
	public string Part1()
	{
		return SolvePuzzle(data, new OldCrane());
	}

	public string Part2()
	{
		return SolvePuzzle(data, new StackCrane());
	}

	private static string SolvePuzzle(IEnumerable<string> data, ICrane crane)
	{
		(var supplyStacks, var moves) = PuzzleParser.Parse(data);
		SupplyStacks boxArea = new SupplyStacks(crane, supplyStacks);
		foreach(var move in moves)
		{
			boxArea.Move(move);
		}

		var top = new List<char>();
		for(int i = 1; i <= boxArea.StackCount; i++)
		{
			var stackTop = boxArea.PeekStack(i);
			top.Add(stackTop);
		}
		var result = string.Concat(top);
		return result;
	}
}