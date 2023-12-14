using Helpers;

namespace Y2023.Day11;

public class Parser
{
	public static List<Coordinate> Parse(IEnumerable<string> lines)
	{
		var parser = new Parser();
		parser.PopulateStarMap(lines);
		return parser.Stars;
	}

	List<Coordinate> Stars { get; } = new();
	private Parser() { }

	public void PopulateStarMap(IEnumerable<string> input)
	{
		int column = 0;
		foreach(var line in input)
		{
			ParseRow(line, column++);
		}
	}

	private void ParseRow(string line, int column)
	{
		for(int x = 0; x < line.Length; x++)
		{
			if(line[x] == '#')
				Stars.Add(new(x, column));
		}
	}
}