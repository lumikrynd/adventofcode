using System.Globalization;
using Y2023.Day18.Models;

namespace Y2023.Day18;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static List<DigStep> Parse(IEnumerable<string> lines)
	{
		return lines
			.Select(ParseLine)
			.ToList();
	}

	public static List<DigStep> AltParse(IEnumerable<string> lines)
	{
		return lines
			.Select(ParseLineAlt)
			.ToList();
	}

	private static DigStep ParseLine(string line)
	{
		var parts = line.Split(' ', splitOptions);
		var direction = ParseDir(parts[0]);
		var length = ParseLength(parts[1]);
		return new(direction, length);
	}

	private static DigStep ParseLineAlt(string line)
	{
		var parts = line.Split(' ', splitOptions);
		return ParseColorCode(parts[2]);
	}

	private static DigStep ParseColorCode(string colorCode)
	{
		var cleaned = colorCode.Trim('#', '(', ')');

		var length = ParseHexLength(cleaned[0..^1]);
		var direction = ParseHexDir(cleaned[^1..]);

		return new(direction, length);
	}

	private static int ParseHexLength(string v) => int.Parse(v, NumberStyles.HexNumber);

	private static Direction ParseHexDir(string v) => v switch
	{
		"0" => Direction.Right,
		"1" => Direction.Down,
		"2" => Direction.Left,
		"3" => Direction.Up,
		_ => throw new NotImplementedException(),
	};

	private static Direction ParseDir(string v) => v switch
	{
		"U" => Direction.Up,
		"D" => Direction.Down,
		"L" => Direction.Left,
		"R" => Direction.Right,
		_ => throw new NotImplementedException(),
	};

	private static int ParseLength(string v) => int.Parse(v);
}