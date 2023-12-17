using NUnit.Framework;
using Y2023.Day12.Models;

namespace Y2023.Day12;

public class ArrangementsCounter
{
	public static long CountArrangements(SpringInfo info)
	{
		var counter = new ArrangementsCounter(info.Conditions, info.SpringGroups);
		return counter.Count();
	}

	readonly Condition[] Conditions;
	readonly int[] Groups;
	readonly int[] RequiredSpace;

	readonly Dictionary<(int, int), long> PreCalculated = new();

	private ArrangementsCounter(Condition[] conditions, IEnumerable<int> groups)
	{
		Conditions = conditions;
		Groups = groups.ToArray();
		RequiredSpace = new int[Groups.Length];

		int space = 0;
		for(int i = Groups.Length - 1; i >= 0; i--)
		{
			space += Groups[i];
			RequiredSpace[i] = space;
			space += 1;
		}
	}

	public long Count()
	{
		return CountArrangements(0, 0);
	}

	private long CountArrangements(int index, int group)
	{
		var preIndex = (index, group);
		if(!PreCalculated.TryGetValue(preIndex, out var result))
			PreCalculated[preIndex] = result = CountArrangementsCalculation(index, group);
		return result;
	}

	private long CountArrangementsCalculation(int index, int group)
	{
		if(group == Groups.Length)
			return RemainingCouldBeEmpty(index) ? 1 : 0;

		if(index + RequiredSpace[group] > Conditions.Length)
			return 0;

		long sum = 0;
		var groupValue = Groups[group];
		var nextIndex = index + groupValue + 1;

		if(CouldBeSpring(index, groupValue))
			sum += CountArrangements(nextIndex, group + 1);

		if(CouldBeEmpty(Conditions[index]))
			sum += CountArrangements(index + 1, group);

		return sum;
	}

	private bool CouldBeSpring(int index, int length)
	{
		if(IsSpring(index + length))
			return false;

		return Enumerable.Range(index, length)
			.All(i => CouldBeSpring(Conditions[i]));
	}

	private bool RemainingCouldBeEmpty(int index) =>
		Conditions.Skip(index).All(CouldBeEmpty);

	private bool IsSpring(int index) =>
		index >= 0 && index < Conditions.Length && Conditions[index] == Condition.Spring;

	private static bool CouldBeSpring(Condition condition) => condition != Condition.Empty;
	private static bool CouldBeEmpty(Condition condition) => condition != Condition.Spring;
}
