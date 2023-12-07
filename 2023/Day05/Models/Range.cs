namespace Y2023.Day05.Models;

public record Range
{
	public required long Start { get; init; }
	public required long Length { get; init; }

	public long End => Start + Length - 1;

	public static Range FromEnd(long start, long end)
	{
		var length = end - start + 1;
		return new Range { Start = start, Length = length };
	}

	public static Range FromLength(long start, long length)
	{
		return new Range { Start = start, Length = length };
	}
}
