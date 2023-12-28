using NUnit.Framework;

namespace Y2023.Day19;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(19114));
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
		Assert.That(result, Is.EqualTo(42));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);
		var interpreter = new Interpreter(model.Workflows);

		long sum = model.Parts
			.Where(interpreter.SortPart)
			.Sum(p => p.X + p.M + p.A + p.S);

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private long Part2(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);
		throw new NotImplementedException();

		//Console.WriteLine($"Result: {result}");
		//return result;
	}
}
