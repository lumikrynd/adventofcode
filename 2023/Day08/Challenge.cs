using Helpers;
using Y2023.Day08.Models;

namespace Y2023.Day08;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var model = Parser.Parse(input);

		string location = "AAA";
		const string goal = "ZZZ";

		long steps = HowLongPath(model, location, l => l == goal);

		return steps.ToString();
	}

	public string Part2()
	{
		var model = Parser.Parse(input);
		var nodeLookup = model.Nodes.ToDictionary(x => x.Id);

		string[] locations = model.Nodes
			.Select(x => x.Id)
			.Where(x => x[^1] == 'A')
			.ToArray();

		static bool IsGoal(string id) => id[^1] == 'Z';

		var steps = locations
			.Select(s => HowLongPath(model, s, IsGoal))
			.ToArray();

		var calculated = LeastCommonMultiple(steps);

		return calculated.ToString();
	}

	private static long HowLongPath(Map model, string start, Func<string, bool> IsGoal)
	{
		var nodeLookup = model.Nodes.ToDictionary(x => x.Id);
		string location = start;

		long steps = 0;
		foreach(var direction in LoopDirections(model.Directions))
		{
			steps++;
			var node = nodeLookup[location];
			if(direction == Direction.Left)
				location = node.Left;
			else if(direction == Direction.Right)
				location = node.Right;

			if(IsGoal(location))
				break;
		}

		return steps;
	}

	static IEnumerable<Direction> LoopDirections(List<Direction> directions)
	{
		while(true)
		{
			foreach(var dir in directions)
			{
				yield return dir;
			}
		}
	}

	private long LeastCommonMultiple(long[] input)
	{
		return input.Aggregate(LeastCommonMultiple);
	}

	private long LeastCommonMultiple(long a, long b)
	{
		return Math.Abs(a * b) / GreatestCommonDivison(a, b);
	}

	private long GreatestCommonDivison(long a, long b)
	{
		while(a != b)
		{
			if(b > a)
				(a, b) = (b, a);

			a = a - b;
		}
		return a;
	}
}