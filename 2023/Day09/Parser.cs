namespace Y2023.Day09;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static List<List<int>> Parse(IEnumerable<string> lines)
	{
		return lines
			.Select(Parse)
			.ToList();
	}

	private static List<int> Parse(string line)
	{
		return line.Split(' ', splitOptions)
			.Select(int.Parse)
			.ToList();
	}
}