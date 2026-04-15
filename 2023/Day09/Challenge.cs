using Helpers;

namespace Y2023.Day09;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var model = Parser.Parse(input);

		long sum = model
			.Select(FindNextInSequence)
			.Sum();

		return sum.ToString();
	}

	private long FindNextInSequence(List<int> sequence)
	{
		List<int> current = sequence;
		List<int> LastOnes = new();
		while(!current.All(i => i == 0))
		{
			LastOnes.Add(current.Last());
			current = DiffSequence(current);
		}

		return LastOnes.Sum();
	}

	public string Part2()
	{
		var model = Parser.Parse(input);

		long sum = model
			.Select(FindPreviousInSequence)
			.Sum();

		return sum.ToString();
	}

	private long FindPreviousInSequence(List<int> sequence)
	{
		List<int> current = sequence;
		List<int> FirstOnes = new();
		while(!current.All(i => i == 0))
		{
			FirstOnes.Add(current.First());
			current = DiffSequence(current);
		}

		var add = FirstOnes
			.Where((_, i) => i % 2 == 0)
			.Sum();

		var subtract = FirstOnes
			.Where((_, i) => i % 2 != 0)
			.Sum();

		return add - subtract;
	}

	private List<int> DiffSequence(List<int> sequence)
	{
		List<int> diffs = new();
		int previous = sequence.First();

		foreach(int i in sequence.Skip(1))
		{
			diffs.Add(i - previous);
			previous = i;
		}

		return diffs;
	}
}