using Helpers;

namespace Y2023.Day03.Models;

public record Map
{
	public required List<Part> Parts { get; init; }
	public required Dictionary<Coordinate, char> Symbols { get; init; }
}
