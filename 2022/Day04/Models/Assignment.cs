namespace Day04.Models;

internal class Assignment
{
	public int Start { get; private set; }
	public int End { get; private set; }

	public Assignment(int start, int end)
	{
		Start = start;
		End = end;
	}

	public bool Overlap(Assignment other)
	{
		var sections = SectionCount() + other.SectionCount();
		var combined = Combine(other);
		return combined.SectionCount() < sections;
	}

	private Assignment Combine(Assignment other)
	{
		int start = Math.Min(Start, other.Start);
		int end = Math.Max(End, other.End);
		return new(start, end);
	}

	private int SectionCount()
	{
		return End - Start + 1;
	}

	public override string ToString()
	{
		return $"{Start},{End}";
	}
}
