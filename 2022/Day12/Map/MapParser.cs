using Helpers;

namespace Day12.Maps;

internal static class MapParser
{
	public static Map ParseMap(string[] lines)
	{
		var X = lines[0].Length;
		var Y = lines.Length;

		var start = FindPositionAndReplace('S', 'a', lines);
		var goal = FindPositionAndReplace('E', 'z', lines);

		int[,] fields = new int[X, Y];

		for (int x = 0, y = 0; y < Y; Next(ref x, ref y, X))
		{
			fields[x, y] = lines[y][x] - 'a';
		}

		return new Map(fields, start, goal);
	}

	private static void Next(ref int x, ref int y, int X)
	{
		x++;
		if (x != X)
			return;

		x = 0;
		y++;
	}

	private static Coordinate FindPositionAndReplace(char target, char replacement, string[] map)
	{
		int y = 0;

		while (true)
		{
			if (!map[y].Contains(target))
			{
				y++;
				continue;
			}

			int x = map[y].IndexOf(target);
			map[y] = map[y].Replace(target, replacement);
			return new(x, y);
		}
	}
}
