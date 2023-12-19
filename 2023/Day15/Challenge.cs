using NUnit.Framework;
using Y2023.Day15.Models;

namespace Y2023.Day15;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(1320));
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
		Assert.That(result, Is.EqualTo(145));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var steps = Parser.Parse(input);

		long sum = 0;
		foreach(var step in steps.Select(s => s.FullStep))
		{
			sum += AsciiStringHelperAlgorithm(step);
		}

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private long Part2(IEnumerable<string> input)
	{
		var steps = Parser.Parse(input);

		var boxes = new List<Lens>[256];
		for(int i = 0; i < 256; i++)
		{
			boxes[i] = new();
		}

		foreach(var step in steps)
		{
			var boxId = AsciiStringHelperAlgorithm(step.Label);
			var box = boxes[boxId];
			if(step.Operation == Operation.Remove)
			{
				Remove(step, box);
			}
			else if(step.Operation == Operation.Set)
			{
				SetLens(step, box);
			}
		}

		long sum = 0;
		for(int i = 0; i < 256; i++)
		{
			int slot = 1;
			foreach(var lens in boxes[i])
			{
				sum += (1 + i) * lens.FocalLength * slot;
				slot++;
			}
		}

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private static void Remove(Step step, List<Lens> box)
	{
		var lens = box.FirstOrDefault(x => x.Label == step.Label);
		if(lens != null)
			box.Remove(lens);
	}

	private static void SetLens(Step step, List<Lens> box)
	{
		var lens = box.FirstOrDefault(x => x.Label == step.Label);
		if(lens != null)
		{
			lens.FocalLength = step.FocalLength;
		}
		else
		{
			lens = new() { Label = step.Label, FocalLength = step.FocalLength };
			box.Add(lens);
		}
	}

	private static int AsciiStringHelperAlgorithm(string s)
	{
		int value = 0;
		foreach(var c in s)
		{
			value += c;
			value *= 17;
			value %= 256;
		}
		return value;
	}
}