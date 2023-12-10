namespace Y2023.Day07.Models;

public record Game
{
	public required char[] Hand { get; init; }
	public required int Bid { get; init; }
}
