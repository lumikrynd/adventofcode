namespace Day04.Models;

internal class AssignmentGroup
{
	private readonly Assignment[] assignments;

	public AssignmentGroup(params Assignment[] assignments)
	{
		this.assignments = assignments;
	}

	public bool OneContainsAll()
	{
		var start = OverallStart();
		var end = OverallEnd();
		return assignments.Any(a => a.Start == start && a.End == end);
	}

	public bool HasOverlaps()
	{
		for(int a1 = 0; a1 < assignments.Length; a1++)
		{
			for(int a2 = a1 + 1; a2 < assignments.Length; a2++)
			{
				if(assignments[a1].Overlap(assignments[a2]))
					return true;
			}
		}

		return false;
	}

	private int OverallStart()
	{
		return assignments.Min(a => a.Start);
	}

	private int OverallEnd()
	{
		return assignments.Max(a => a.End);
	}
}
