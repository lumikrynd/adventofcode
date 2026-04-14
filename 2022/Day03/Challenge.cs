using Helpers;
using NUnit.Framework;

namespace Y2022.Day03;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("157"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("70"));
	}
}

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