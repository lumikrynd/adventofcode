namespace Y2023.Day08.Models;

public record Node
{
	public required string Id { get; init; }
	public required string Left { get; init; }
	public required string Right { get; init; }
}
