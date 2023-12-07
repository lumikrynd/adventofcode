namespace Y2023.Day05.Models;

public record Almanac
{
	public required List<long> Seeds { get; init; }
	public required List<Conversion> SeedToSoil {get; init; }
	public required List<Conversion> SoilToFertilizer {get; init; }
	public required List<Conversion> FertilizerToWater {get; init; }
	public required List<Conversion> WaterToLight {get; init; }
	public required List<Conversion> LightToTemperature {get; init; }
	public required List<Conversion> TemperatureToHumidity {get; init; }
	public required List<Conversion> HumidityToLocation {get; init; }

	public IEnumerable<Range> GetSeedRanges()
	{
		for(int i = 0; i <  Seeds.Count; i += 2)
		{
			var range = Range.FromLength(Seeds[i], Seeds[i + 1]);
			yield return range;
		}
	}

	public List<List<Conversion>> Conversions()
	{
		List<List<Conversion>> result = new()
		{
			SeedToSoil,
			SoilToFertilizer,
			FertilizerToWater,
			WaterToLight,
			LightToTemperature,
			TemperatureToHumidity,
			HumidityToLocation,
		};

		return result;
	}
}
