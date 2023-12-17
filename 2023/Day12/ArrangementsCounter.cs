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

	private long CountArrangements(int startIndex, int group)
	{
		if(group == Groups.Length)
			return RemainingCouldBeEmpty(startIndex) ? 1 : 0;

		List<long> sums = new();
		var groupValue = Groups[group];

		for(int i = startIndex; (i + RequiredSpace[group] - 1) < Conditions.Length; i++)
		{
			long count;

			if(PreCalculated.TryGetValue((i, group), out count))
			{
				sums.Add(count);
				break;
			}

			if(CouldBeSpring(i, groupValue))
			{
				var nextIndex = i + groupValue + 1;
				count = CountArrangements(nextIndex, group + 1);
			}
			sums.Add(count);

			if(Conditions[i] == Condition.Spring)
				break;
		}

		long sum = 0;
		for(int i = sums.Count - 1; i >= 0; i--)
		{
			sum += sums[i];
			int sIndex = startIndex + i;
			PreCalculated[(sIndex, group)] = sum;
		}

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
