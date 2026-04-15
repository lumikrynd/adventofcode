using Helpers;

namespace Y2022.Day04;

public class Challenge(IEnumerable<string> data) : ISolver
{
	public string Part1()
	{
		var groups = AssignmentGroupParser.Parse(data);

		var containedCount = groups.Count(g => g.OneContainsAll());
		return $"{containedCount}";
	}

	public string Part2()
	{
		var groups = AssignmentGroupParser.Parse(data);

		var overlapCount = groups.Count(g => g.HasOverlaps());
		return $"{overlapCount}";
	}
}