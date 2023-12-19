namespace Y2023.Day15.Models;

public record Step
{
	public required string FullStep { get; init; }
	public required string Label { get; init; }
	public required Operation Operation { get; init; }
	public int FocalLength { get; init; }
}
