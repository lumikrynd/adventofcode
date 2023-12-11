using NUnit.Framework;

namespace Y2023.Day09;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(114));
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
		Assert.That(result, Is.EqualTo(2));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);

		long sum = model
			.Select(FindNextInSequence)
			.Sum();

		Console.Write($"Result: {sum}");
		return sum;
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

	private long Part2(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);

		long sum = model
			.Select(FindPreviousInSequence)
			.Sum();

		Console.Write($"Result: {sum}");
		return sum;
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