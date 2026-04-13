using Helpers;

namespace Y2022.Day03;

internal class Program
{
	static void Main(string[] args)
	{
		var example_input = File.ReadLines(@"Data/Example.txt");
		var example = new Challenge(example_input);

		Console.WriteLine(@"Part 1");
		Console.WriteLine(example.Part1());
		Console.WriteLine();

		Console.WriteLine(@"Part 2");
		Console.WriteLine(example.Part2());
		Console.WriteLine();
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

		return 	$"PrioritySum: {prioritySum}";
	}

	public string Part2()
	{
		int prioritySum = 0;

		var groups = BackpackerGroupParser.ParseBackpackerGroups(input);
		foreach(var group in groups)
		{
			prioritySum += group.GetRearrangementPriority();
		}

		return $"PrioritySum: {prioritySum}";
	}
}