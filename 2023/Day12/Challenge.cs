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

	private long Part1(IEnumerable<string> input)
	{
		var infos = Parser.Parse(input);

		long sum = 0;
		foreach(var info in infos)
		{
			sum += CountArrangements(info);
			Console.WriteLine(sum);
		}

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private long Part2(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);
		var unfolded = model.Select(Unfold).ToList();

		long sum = 0;
		foreach(var info in unfolded)
		{
			sum += CountArrangements(info);
		}

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private SpringInfo Unfold(SpringInfo info)
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

	private long CountArrangements(SpringInfo info)
	{
		var groups = new LinkedList<int>(info.SpringGroups);
		var conditions = info.Conditions;

		return CountArrangements(groups.First, new ArraySegment<Condition>(conditions));
	}

	private long CountArrangements(LinkedListNode<int>? group, ArraySegment<Condition> conditions)
	{
		if(group is null)
			return CouldBeEmpty(conditions) ? 1 : 0;

		long sum = 0;

		for(int i = 0; (i + group.Value - 1) < conditions.Count; i++)
		{
			if(CouldBeSpring(conditions, i, group.Value))
			{
				var remainingConditions = NextArraySegment(conditions, i, group.Value);
				sum += CountArrangements(group.Next, remainingConditions);
			}
		}

		return sum;
	}

	ArraySegment<Condition> NextArraySegment(ArraySegment<Condition> previous, int index, int length)
	{
		var array = previous.Array!;
		var newOffset = previous.Offset + index + length + 1;
		var newCount = array.Length - newOffset;
		if(newCount < 1)
			return ArraySegment<Condition>.Empty;

		return new ArraySegment<Condition>(array, newOffset, newCount);
	}

	private bool CouldBeSpring(IList<Condition> list, int index, int length)
	{
		if(list.Count < index + length)
			return false;

		for(int i = 0; i < index; i++)
		{
			if(list[i] == Condition.Spring)
				return false;
		}
		if(IsSpring(list, index + length))
			return false;

		return Enumerable.Range(index, length)
			.All(i => CouldBeSpring(list[i]));
	}

	private bool CouldBeEmpty(IList<Condition> list)
	{
		return list.All(CouldBeEmpty);
	}

	private bool IsSpring(IList<Condition> list, int index) =>
		index >= 0 && index < list.Count && list[index] == Condition.Spring;

	private bool CouldBeSpring(Condition condition) => condition != Condition.Empty;
	private bool CouldBeEmpty(Condition condition) => condition != Condition.Spring;
}
