using Helpers;
using NUnit.Framework;
using Y2023.Day12.Models;

namespace Y2023.Day12;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("21"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("525152"));
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var infos = Parser.Parse(input);
		return CountAllArrangements(infos).ToString();
	}

	public string Part2()
	{
		var model = Parser.Parse(input);
		var unfolded = model.Select(Unfold).ToList();
		return CountAllArrangements(unfolded).ToString();
	}

	private static long CountAllArrangements(List<SpringInfo> infos)
	{
		long sum = 0;
		foreach(var info in infos)
		{
			sum += ArrangementsCounter.CountArrangements(info);
		}

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