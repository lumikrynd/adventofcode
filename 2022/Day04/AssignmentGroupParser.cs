using System.Numerics;
using Day04.Models;

namespace Day04;

internal class AssignmentGroupParser
{
	public static IEnumerable<AssignmentGroup> Parse(IEnumerable<string> rawData)
	{
		var parser = new AssignmentGroupParser(rawData);
		return parser.Result;
	}

	private IEnumerable<string> rawData;
	private readonly List<AssignmentGroup> assignmentGroups = new();

	public IEnumerable<AssignmentGroup> Result => assignmentGroups;

	private AssignmentGroupParser(IEnumerable<string> rawData)
	{
		this.rawData = rawData;
		Parse();
	}

	private void Parse()
	{
		foreach(var rawPair in rawData)
		{
			var group = ParseGroup(rawPair);
			assignmentGroups.Add(group);
		}
	}

	private AssignmentGroup ParseGroup(string rawPair)
	{
		string[] ranges = rawPair.Split(',');
		var assignment1 = ParseAssignment(ranges[0]);
		var assignment2  = ParseAssignment(ranges[1]);
		return new AssignmentGroup(assignment1, assignment2);
	}

	private Assignment ParseAssignment(string rawAssignment)
	{
		MakeSureFormatIsSupported(rawAssignment);

		string[] range = rawAssignment.Split("-");
		var start = Int32.Parse(range[0]);
		var end = Int32.Parse(range[1]);
		return new Assignment(start, end);
	}

	private void MakeSureFormatIsSupported(string rawAssignment)
	{
		if(rawAssignment.Count(c => c == '-') != 1)
			throw new Exception("... needs to support negatives apparrantly"); ;
	}
}
