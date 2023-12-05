using Helpers;

namespace Y2023.Day03.Models;

public record Part
{
	public required int Value { get; init; }
	public required List<Coordinate> Coordinates { get; init; }
}
