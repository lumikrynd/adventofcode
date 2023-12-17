using Helpers;

namespace Y2023.Day14.Models;

public record ParsedInput
{
	public required List<Coordinate> RoundRocks { get; init; }
	public required List<Coordinate> SquareRocks { get; init; }
	public required int Width { get; init; }
	public required int Height { get; init; }
}
