using NUnit.Framework;

namespace Y2023.Template;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		Part1(ExampleInput);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		Part2(ExampleInput);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private void Part1(IEnumerable<string> input)
	{
		throw new NotImplementedException();
	}

	private void Part2(IEnumerable<string> input)
	{
		throw new NotImplementedException();
	}
}