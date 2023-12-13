using Helpers;
using Y2023.Day10.Models;

namespace Y2023.Day10;

public class Parser
{
	public static Map Parse(IEnumerable<string> input)
	{
		int height = input.Count();
		int width = input.First().Length;
		var parser = new Parser(width, height);

		parser.ParseMap(input);
		return parser.Result();
	}

	private PipeType[,] pipes;
	private Coordinate? start = null;

	public Parser(int width, int height)
	{
		pipes = new PipeType[width, height];
	}

	public Map Result()
	{
		return new Map()
		{
			Pipes = pipes,
			Start = start!,
		};
	}

	public void ParseMap(IEnumerable<string> input)
	{
		int y = 0;
		foreach(var line in input)
		{
			AddRow(line, y++);
		}
	}

	private void AddRow(string line, int y)
	{
		for(int x = 0; x < line.Length; x++)
		{
			var pipe = Parse(line[x]);
			pipes[x, y] = pipe;

			if(pipe == PipeType.Start)
				start = new Coordinate(x, y);
		}
	}

	private static PipeType Parse(char c) => c switch
	{
		'.' => PipeType.None,
		'S' => PipeType.Start,
		'|' => PipeType.Vertical,
		'-' => PipeType.Horizontal,
		'L' => PipeType.UpToRight,
		'J' => PipeType.UpToLeft,
		'7' => PipeType.DownToLeft,
		'F' => PipeType.DownToRight,
		_ => throw new NotImplementedException(),
	};

/*
| is a vertical pipe connecting north and south.
- is a horizontal pipe connecting east and west.
L is a 90-degree bend connecting north and east.
J is a 90-degree bend connecting north and west.
7 is a 90-degree bend connecting south and west.
F is a 90-degree bend connecting south and east.
. is ground; there is no pipe in this tile.
S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
 */
}