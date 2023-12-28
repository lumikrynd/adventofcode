using Helpers;
using NUnit.Framework;
using Y2023.Day18.Models;

namespace Y2023.Day18;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(62));
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
		Assert.That(result, Is.EqualTo(952408144115));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var digPlan = Parser.Parse(input);
		var relDigPlan = MakeRelative(digPlan);

		long lavaArea = Digger.CalculateLavaArea(relDigPlan); ;

		Console.WriteLine($"Result: {lavaArea}");
		return lavaArea;
	}

	private long Part2(IEnumerable<string> input)
	{
		var digPlan = Parser.AltParse(input);
		var relDigPlan = MakeRelative(digPlan);

		long lavaArea = Digger.CalculateLavaArea(relDigPlan); ;

		Console.WriteLine($"Result: {lavaArea}");
		return lavaArea;
	}

	private List<RelativeDigStep> MakeRelative(List<DigStep> digPlan)
	{
		return digPlan.ToCircularList()
			.Select(n => MakeRelative(n.Item, n.Next.Item))
			.ToList();
	}

	private RelativeDigStep MakeRelative(DigStep current, DigStep next)
	{
		return new(current.Length, CalculateTurn(current.Direction, next.Direction));
	}

	private Turn CalculateTurn(Direction curDir, Direction nextDir)
	{
		if(nextDir == curDir.SpinClockwise())
			return Turn.Right;
		if(nextDir == curDir.SpinCounterClockwise())
			return Turn.Left;

		throw new ArgumentException("Something is wrong");
	}
}