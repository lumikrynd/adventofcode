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
