namespace Y2023.Day05.Models;

public record Conversion
{
	public required long DestionationStart {get; init; }
	public required long SourceStart {get; init; }
	public required long Length {get; init; }

	public long SourceEnd => SourceStart + Length - 1;
}
