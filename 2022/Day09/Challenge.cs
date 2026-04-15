using Helpers;

namespace Y2022.Day09;

public class Challenge(IEnumerable<string> puzzleInput) : ISolver
{
	public string Part1()
	{
		return CalculateWithRopeLength(2).ToString();
	}

	public string Part2()
	{
		return CalculateWithRopeLength(10).ToString();
	}

	private int CalculateWithRopeLength(int ropeLength)
	{
		var moves = MovesParser.Parse(puzzleInput);
		RopeEmulator re = new RopeEmulator(moves, ropeLength);
		int visitedCount = re.Visited.Count;
		return visitedCount;
	}
}