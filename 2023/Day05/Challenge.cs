using Helpers;
using NUnit.Framework;

namespace Y2023.Day05;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day05/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("35"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("46"));
	}
}

public class Challenge(params IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var almanac = Parser.ParseAlmanac(input);
		var converter = Converter.FromAlmanac(almanac);

		var finalValues = almanac.Seeds
			.Select(converter.Convert)
			.ToList();

		var result = finalValues.Min();
		return result.ToString();
	}

	public string Part2()
	{
		var almanac = Parser.ParseAlmanac(input);
		var converter = Converter.FromAlmanac(almanac);

		var seedRanges = almanac.GetSeedRanges().ToList();
		var converted = converter.ConvertRanges(seedRanges);

		var result = converted.Min(x => x.Start);
		return result.ToString();
	}
}