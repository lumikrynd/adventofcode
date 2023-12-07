using NUnit.Framework;

namespace Y2023.Day05;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(35));
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
		Assert.That(result, Is.EqualTo(46));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var almanac = Parser.ParseAlmanac(input);
		var converter = Converter.FromAlmanac(almanac);

		var finalValues = almanac.Seeds
			.Select(converter.Convert)
			.ToList();

		var result = finalValues.Min();

		Console.Write($"Result: {result}");
		return result;
	}

	private long Part2(IEnumerable<string> input)
	{
		var almanac = Parser.ParseAlmanac(input);
		var converter = Converter.FromAlmanac(almanac);

		var seedRanges = almanac.GetSeedRanges().ToList();
		var converted = converter.ConvertRanges(seedRanges);

		var result = converted.Min(x => x.Start);

		Console.Write($"Result: {result}");
		return result;
	}
}
