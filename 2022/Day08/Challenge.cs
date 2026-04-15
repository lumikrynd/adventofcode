using Helpers;

namespace Y2022.Day08;

public class Challenge(IEnumerable<string> puzzleInput) : ISolver
{
	public string Part1() => Part1_V1();

	public string Part1_V1()
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);

		var count = groove.CountVisibleTrees();

		return count.ToString();
	}

	public string Part1_V2()
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);

		var count = groove.CountVisibleTreesV2();

		return count.ToString();
	}

	public string Part2()
	{
		var groove = TreeGrooveParser.Parse(puzzleInput);
		(int value, var coordinate) = ScenicSpotFinder.FindScenicSpotValue(groove);
		return value.ToString();
	}
}