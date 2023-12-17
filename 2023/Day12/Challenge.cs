using NUnit.Framework;
using Y2023.Day12.Models;

namespace Y2023.Day12;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(21));
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		Assert.That(result, Is.EqualTo(525152));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private static long Part1(IEnumerable<string> input)
	{
		var infos = Parser.Parse(input);
		return CountAllArrangements(infos);
	}

	private static long Part2(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);
		var unfolded = model.Select(Unfold).ToList();
		return CountAllArrangements(unfolded);
	}

	private static long CountAllArrangements(List<SpringInfo> infos)
	{
		long sum = 0;
		foreach(var info in infos)
		{
			sum += ArrangementsCounter.CountArrangements(info);
			Console.WriteLine(sum);
		}

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private static SpringInfo Unfold(SpringInfo info)
	{
		var newGroups = Enumerable.Repeat(info.SpringGroups, 5)
			.SelectMany(x => x)
			.ToList();

		var newConditions = info.Conditions.ToList();
		for(int i = 0; i < 4; i++)
		{
			newConditions.Add(Condition.Unknown);
			newConditions.AddRange(info.Conditions);
		}

		return new()
		{
			SpringGroups = newGroups,
			Conditions = newConditions.ToArray(),
		};
	}
}

public class ArrangementsCounter
{
	public static long CountArrangements(SpringInfo info)
	{
		var counter = new ArrangementsCounter(info.Conditions, info.SpringGroups);
		return counter.Count();
	}

	readonly Condition[] Conditions;
	readonly int[] Groups;

	private ArrangementsCounter(Condition[] conditions, IEnumerable<int> groups)
	{
		Conditions = conditions;
		Groups = groups.ToArray();
	}

	public long Count()
	{
		return CountArrangements(0, new ArraySegment<Condition>(Conditions));
	}

	private long CountArrangements(int group, ArraySegment<Condition> conditions)
	{
		if(group == Groups.Length)
			return CouldBeEmpty(conditions) ? 1 : 0;

		long sum = 0;
		var groupValue = Groups[group];

		for(int i = 0; (i + groupValue - 1) < conditions.Count; i++)
		{
			if(CouldBeSpring(conditions, i, groupValue))
			{
				var remainingConditions = NextArraySegment(conditions, i, groupValue);
				sum += CountArrangements(group + 1, remainingConditions);
			}

			if(conditions[i] == Condition.Spring)
				break;
		}

		return sum;
	}

	private static ArraySegment<Condition> NextArraySegment(ArraySegment<Condition> previous, int index, int length)
	{
		var array = previous.Array!;
		var newOffset = previous.Offset + index + length + 1;
		var newCount = array.Length - newOffset;
		if(newCount < 1)
			return ArraySegment<Condition>.Empty;

		return new ArraySegment<Condition>(array, newOffset, newCount);
	}

	private static bool CouldBeSpring(IList<Condition> list, int index, int length)
	{
		if(list.Count < index + length)
			return false;

		if(IsSpring(list, index + length))
			return false;

		return Enumerable.Range(index, length)
			.All(i => CouldBeSpring(list[i]));
	}

	private static bool CouldBeEmpty(IList<Condition> list)
	{
		return list.All(CouldBeEmpty);
	}

	private static bool IsSpring(IList<Condition> list, int index) =>
		index >= 0 && index < list.Count && list[index] == Condition.Spring;

	private static bool CouldBeSpring(Condition condition) => condition != Condition.Empty;
	private static bool CouldBeEmpty(Condition condition) => condition != Condition.Spring;
}
