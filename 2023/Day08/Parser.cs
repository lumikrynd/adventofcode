using Y2023.Day08.Models;

namespace Y2023.Day08;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static Map Parse(IEnumerable<string> lines)
	{
		var directions = ParseDirections(lines.First());
		var nodes = ParseNodes(lines.Skip(2));

		return new()
		{
			Directions = directions,
			Nodes = nodes,
		};
	}

	private static List<Direction> ParseDirections(string v)
	{
		return v.Select(ParseDirection).ToList();
	}

	private static Direction ParseDirection(char c) => c switch
	{
		'L' => Direction.Left,
		'R' => Direction.Right,
		_ => throw new NotImplementedException(),
	};

	private static List<Node> ParseNodes(IEnumerable<string> input)
	{
		return input.Select(ParseNode).ToList();
	}

	private static Node ParseNode(string input)
	{
		char[] separators = { ' ', '=', '(', ')', ',' };
		var parts = input.Split(separators, splitOptions);

		return new()
		{
			Id = parts[0],
			Left = parts[1],
			Right = parts[2],
		};
	}
}
