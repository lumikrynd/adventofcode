using Helpers;

namespace Y2023.Day10.Models;

public record Map
{
	public required Coordinate Start { get; init; }
	public required PipeType[,] Pipes { get; init; }
}
