using NUnit.Framework;

namespace Day04;

public class Tests
{
	static IEnumerable<string> ExampleData => File.ReadLines(@"Data/Example.txt");
	static IEnumerable<string> PuzzleData => File.ReadLines(@"Data/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		Part1(ExampleData);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleData);
	}

	[Test]
	public void Part2_Example()
	{
		Part2(ExampleData);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleData);
	}

	public void Part1(IEnumerable<string> data)
	{
		var groups = AssignmentGroupParser.Parse(data);

		var containedCount = groups.Count(g => g.OneContainsAll());
		Console.WriteLine($"Fully contained count: {containedCount}");
	}

	public void Part2(IEnumerable<string> data)
	{
		var groups = AssignmentGroupParser.Parse(data);

		var overlapCount = groups.Count(g => g.HasOverlaps());
		Console.WriteLine($"Has overlaps count: {overlapCount}");
	}
}