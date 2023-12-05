namespace Y2023.Day02.Models;

public record Set
{
	public required List<(Color Color, int Count)> ColorCounts { get; init; }

	public bool IsSupersetOff(Set other)
	{
		foreach(var item in other.ColorCounts)
		{
			var thisCount = ColorCounts.FirstOrDefault(x => x.Color == item.Color);
			if(item.Count > thisCount.Count)
				return false;
		}

		return true;
	}
}
