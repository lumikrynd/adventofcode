using Helpers;
using Y2023.Day03.Models;

namespace Y2023.Day03;

internal class Parser
{
	public static Map ParseMap(IEnumerable<string> lines)
	{
		var parser = new Parser();
		parser.CalculateMaps(lines);
		return parser.Result();
	}

	List<Part> parts = new();
	Dictionary<Coordinate, char> symbols = new();

	private Parser() { } 

	public void CalculateMaps(IEnumerable<string> lines)
	{
		int lineNumber = 0;
		foreach(var line in lines)
		{
			ParseLine(line, lineNumber++);
		}
	}

	private void ParseLine(string line, int y)
	{
		for(int x = 0; x < line.Length; x++)
		{
			if(line[x] == '.')
				continue;

			if(IsDigit(line[x]))
			{
				ParseNumber(line, ref x, y);
				x--;
				continue;
			}

			var coord = new Coordinate(x, y);
			symbols[coord] = line[x];
		}
	}

	private void ParseNumber(string line, ref int x, int y)
	{
		List<Coordinate> coordinates = new();
		int value = 0;
		for(; x < line.Length; x++)
		{
			char c = line[x];
			if(!IsDigit(c))
				break;

			coordinates.Add(new Coordinate(x, y));
			value = (10 * value) + (c - '0');
		}

		var part = new Part()
		{
			Value = value,
			Coordinates = coordinates,
		};

		parts.Add(part);
	}

	private static bool IsDigit(char c) => c >= '0' && c <= '9';

	public Map Result()
	{
		return new Map()
		{
			Parts = parts,
			Symbols = symbols,
		};
	}
}
