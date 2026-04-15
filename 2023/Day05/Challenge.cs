using Helpers;

namespace Y2023.Day05;

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