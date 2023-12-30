using NUnit.Framework;

namespace Y2023.Day20;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Example2.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(32000000));
	}

	[Test]
	public void Part1_Example2()
	{
		var result = Part1(ExampleInput2);
		Assert.That(result, Is.EqualTo(11687500));
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	//[Test]
	//public void Part2_Example()
	//{
	//	var result = Part2(ExampleInput);
	//	Assert.That(result, Is.EqualTo(42));
	//}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var modules = Parser.Parse(input);
		var emulator = new Emulator(modules);

		emulator.PushButton(1000);

		var counts = emulator.GetCounts();
		var result = 1L * counts.Low * counts.High;

		Console.WriteLine($"Result: {result}");
		return result;
	}

	private long Part2(IEnumerable<string> input)
	{
		var modules = Parser.Parse(input);
		var emulator = new Emulator(modules);

		throw new NotImplementedException();
		//Console.WriteLine($"Result: {presses}");
		//return presses;
	}
}
