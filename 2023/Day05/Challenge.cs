using NUnit.Framework;
using Y2023.Day05.Models;

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
		Part2(ExampleInput);
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

	private void Part2(IEnumerable<string> input)
	{
		throw new NotImplementedException();
	}
}

public class Converter
{
	readonly Dictionary<long, Conversion> Conversions;
	readonly long[] SortedSources;
	readonly Converter? InnerConverter;

	public static Converter FromAlmanac(Almanac almanac)
	{
		var conversions = almanac.Conversions();
		conversions.Reverse();

		Converter? converter = null;
		foreach(var conversion in conversions)
		{
			converter = new Converter(conversion, converter);
		}

		return converter ?? new(new());
	}

	public Converter(List<Conversion> conversions) : this(conversions, null)
	{
	}

	private Converter(List<Conversion> conversions, Converter? innerConverter)
	{
		Conversions = conversions.ToDictionary(x => x.SourceStart);
		InnerConverter = innerConverter;

		var sources = conversions.Select(x => x.SourceStart).ToArray();
		Array.Sort(sources);
		SortedSources = sources;
	}

	public long Convert(long value)
	{
		var newValue = SingleConvert(value);
		if(InnerConverter is null)
			return newValue;

		return InnerConverter.Convert(newValue);
	}

	private long SingleConvert(long value)
	{
		var index = Array.BinarySearch(SortedSources, value);
		if(index < 0)
		{
			index = ~index;
			index -= 1;
		}

		if(index < 0)
			return value;

		var conversion = Conversions[SortedSources[index]];

		if(value >= conversion.SourceStart + conversion.Length)
			return value;

		return value - conversion.SourceStart + conversion.DestionationStart;
	}
}