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
	readonly int[] RequiredSpace;

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

		long sum = 0;
		var groupValue = Groups[group];

		for(int i = startIndex; (i + RequiredSpace[group] - 1) < Conditions.Length; i++)
		{
			if(CouldBeSpring(i, groupValue))
			{
				var nextIndex = i + groupValue + 1;
				sum += CountArrangements(nextIndex, group + 1);
			}

			if(Conditions[i] == Condition.Spring)
				break;
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
