namespace Y2023.Day17;

public class Parser
{
	public static int[,] Parse(IEnumerable<string> lines)
	{
		var parsedLines = lines.Select(ParseLine).ToList();
		int width = parsedLines.First().Length;
		int height = parsedLines.Count;

		var map = new int[width, height];
		for(int y = 0; y < height; y++)
		{
			for(int x = 0; x < width; x++)
			{
				map[x, y] = parsedLines[y][x];
			}
		}

		return map;
	}

	private static int[] ParseLine(string line)
	{
		return line
			.Select(c => c - '0')
			.ToArray();
	}
}