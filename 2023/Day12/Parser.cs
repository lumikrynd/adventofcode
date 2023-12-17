using Y2023.Day12.Models;

namespace Y2023.Day12;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static List<SpringInfo> Parse(IEnumerable<string> lines)
	{
		return lines.Select(ParseLine).ToList();
	}

	private static SpringInfo ParseLine(string line)
	{
		var parts = line.Split(' ', splitOptions);
		return new()
		{
			Conditions = ParseConditions(parts[0]),
			SpringGroups = ParseGroups(parts[1]),
		};
	}

	private static Condition[] ParseConditions(string v)
	{
		return v.Select(ParseCondition).ToArray();
	}

	private static Condition ParseCondition(char c) => c switch
	{
		'.' => Condition.Empty,
		'#' => Condition.Spring,
		'?' => Condition.Unknown,
		_ => throw new NotImplementedException(),
	};

	private static List<int> ParseGroups(string v)
	{
		return v.Split(',', splitOptions)
			.Select(int.Parse)
			.ToList();
	}
}
