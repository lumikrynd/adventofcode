using Y2022.Day05.Models;

namespace Y2022.Day05.Parsers;

internal class PuzzleParser
{
	public static (Stack<char>[], List<Move>) Parse(IEnumerable<string> input)
	{
		var stackInput = input.TakeWhile(x => x != "");
		var movesInput = input.SkipWhile(x => x != "").Skip(1);
		var supplyStack = StacksParser.Parse(stackInput);
		var moves = MovesParser.Parse(movesInput);
		return (supplyStack, moves);
	}
}