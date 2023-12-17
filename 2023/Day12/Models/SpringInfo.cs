namespace Y2023.Day12.Models;

public record SpringInfo
{
	public required Condition[] Conditions { get; init; }
	public required List<int> SpringGroups { get; init; }
}
