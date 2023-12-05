namespace Y2023.Day02.Models;

public record Game
{
	public required int GameId { get; init; }
	public required List<Set> Sets { get; init; }
}
