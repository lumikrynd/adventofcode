using Y2022.Day11.Models;
using Y2022.Day11.Parsing;
using Helpers;

namespace Y2022.Day11;

public class Challenge(IEnumerable<string> puzzleInput) : ISolver
{
	public string Part1()
	{
		var parser = MonkeyListParser.CreateParser();
		var monkeys = parser.Parse(puzzleInput.ToList());
		for(int i = 0; i < 20; i++)
		{
			foreach(var monkey in monkeys)
			{
				monkey.InspectItems();
				monkey.PostInspectRelief();
				monkey.ThrowItems(monkeys);
			}
		}

		for(int i = 0; i < monkeys.Count; i++)
		{
			var monkey = monkeys[i];
		}

		var SortedInspectCounts = monkeys
			.Select(x => x.InspectCount)
			.OrderByDescending(x => x)
			.ToList();

		var result = SortedInspectCounts[0] * SortedInspectCounts[1];

		return result.ToString();
	}

	public string Part2()
	{
		var parser = MonkeyListParser.CreateParser();
		var monkeys = parser.Parse(puzzleInput.ToList());
		var leastCommonMultiple = monkeys
			.Select(x => x.DivisorTest)
			.Aggregate(1, LeastCommonMultiple);

		for(int i = 1; i <= 10000; i++)
		{
			foreach(var monkey in monkeys)
			{
				monkey.InspectItems();
				monkey.ReduceWorryByDivisor(leastCommonMultiple);
				monkey.ThrowItems(monkeys);
			}
			LogMonkeys(i, monkeys);
		}

		for(int i = 0; i < monkeys.Count; i++)
		{
			var monkey = monkeys[i];
		}

		var SortedInspectCounts = monkeys
			.Select(x => x.InspectCount)
			.OrderByDescending(x => x)
			.ToList();

		var result = (long)SortedInspectCounts[0] * SortedInspectCounts[1];
		return result.ToString();
	}

	private void LogMonkeys(int round, List<Monkey> monkeys)
	{
		if(round is 1 or 20 || round % 1000 == 0)
		{
			for(int i = 0; i < monkeys.Count; i++)
			{
				var monkey = monkeys[i];
			}
		}
	}

	static int LeastCommonMultiple(int a, int b)
	{
		return (a / GreatestCommonFactor(a, b)) * b;
	}

	static int GreatestCommonFactor(int a, int b)
	{
		while (b != 0)
		{
			int temp = b;
			b = a % b;
			a = temp;
		}
		return a;
	}
}