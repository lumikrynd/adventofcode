using Helpers;
using Y2023.Day05.Models;

namespace Y2023.Day05;

public class Parser
{
	public static Almanac ParseAlmanac(IEnumerable<string> lines)
	{
		var parser = new Parser(lines);
		parser.Parse();
		return parser.Result();
	}

	List<long> Seeds = new();
	List<Conversion> SeedToSoil = new();
	List<Conversion> SoilToFertilizer = new();
	List<Conversion> FertilizerToWater = new();
	List<Conversion> WaterToLight = new();
	List<Conversion> LightToTemperature = new();
	List<Conversion> TemperatureToHumidity = new();
	List<Conversion> HumidityToLocation = new();

	EnumeratorWrapper<string> Lines;

	private Parser(IEnumerable<string> lines)
	{
		Lines = new(lines.GetEnumerator());
	}

	public Almanac Result()
	{
		return new Almanac()
		{
			Seeds = Seeds,
			SeedToSoil = SeedToSoil,
			SoilToFertilizer = SoilToFertilizer,
			FertilizerToWater = FertilizerToWater,
			WaterToLight = WaterToLight,
			LightToTemperature = LightToTemperature,
			TemperatureToHumidity = TemperatureToHumidity,
			HumidityToLocation = HumidityToLocation,
		};
	}

	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public void Parse()
	{
		ParseSeeds();
		SeedToSoil = ParseSection();
		SoilToFertilizer = ParseSection();
		FertilizerToWater = ParseSection();
		WaterToLight = ParseSection();
		LightToTemperature = ParseSection();
		TemperatureToHumidity = ParseSection();
		HumidityToLocation = ParseSection();
	}

	private void ParseSeeds()
	{
		var line = Lines.Current;
		var parts = line.Split(':', splitOptions);
		Seeds = ParseNumbers(parts[1]);

		SkipToNextSection();
	}

	private List<Conversion> ParseSection()
	{
		var conversions = new List<Conversion>();
		while(!string.IsNullOrWhiteSpace(NullablePeek()))
		{
			var current = Lines.GetNext();
			var conversion = ParseConversion(current);
			conversions.Add(conversion);
		}
		SkipToNextSection();

		return conversions;
	}

	private static Conversion ParseConversion(string s)
	{
		var numbers = ParseNumbers(s);
		return new()
		{
			DestionationStart = numbers[0],
			SourceStart = numbers[1],
			Length = numbers[2],
		};
	}

	private string? NullablePeek()
	{
		return Lines.TryPeek(out string ? result) ? result : null;
	}

	private void SkipToNextSection()
	{
		if(!Lines.HasNext())
			return;
		Lines.MoveNext();
		Lines.MoveNext();
	}

	private static List<long> ParseNumbers(string line)
	{
		return line
			.Split(' ', splitOptions)
			.Select(long.Parse)
			.ToList();
	}
}