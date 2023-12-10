namespace Y2023.Day08.Models;

public record Map
{
	public required List<Direction> Directions { get; init; }
	public required List<Node> Nodes { get; init; }
}
