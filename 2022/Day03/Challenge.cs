using Helpers;

namespace Y2022.Day03;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		int prioritySum = 0;

		var backpacks = BackpackParser.ParseBackpacks(input);
		foreach(var backpack in backpacks)
		{
			prioritySum += backpack.GetRearrangementPriority();
		}

		return prioritySum.ToString();
	}

	public string Part2()
	{
		int prioritySum = 0;

		var groups = BackpackerGroupParser.ParseBackpackerGroups(input);
		foreach(var group in groups)
		{
			prioritySum += group.GetRearrangementPriority();
		}

		return prioritySum.ToString();
	}
}