namespace Y2023.Day06;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static (int time, int distance)[] Parse(IEnumerable<string> lines)
	{
		var l = lines.ToArray();
		var times = ParseNumbers(l[0]);
		var distances = ParseNumbers(l[1]);

		return times.Zip(distances).ToArray();
	}

	private static int[] ParseNumbers(string s)
	{
		return s.Split(' ', splitOptions)
			.Skip(1)
			.Select(int.Parse)
			.ToArray();
	}

	public static (long time, long distance) ParseSingle(IEnumerable<string> lines)
	{
		var l = lines.ToArray();
		var time = ParseNumber(l[0]);
		var distance = ParseNumber(l[1]);
		return (time, distance);
	}

	private static long ParseNumber(string s)
	{
		var parts = s.Split(' ', splitOptions).Skip(1);
		var single = string.Join("", parts);
		return long.Parse(single);
	}
}