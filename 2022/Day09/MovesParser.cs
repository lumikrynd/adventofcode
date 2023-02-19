namespace Day09;

internal class MovesParser
{
	public static List<Move> Parse(IEnumerable<string> rawData)
	{
		List<Move> moves = new List<Move>();
		foreach(var line in rawData)
		{
			Move move = ParseMove(line);
			moves.Add(move);
		}
		return moves;
	}

	private static Move ParseMove(string line)
	{
		var parts = line.Split();
		var dir = ParseDirection(parts[0]);
		var length = int.Parse(parts[1]);
		return new Move(dir, length);
	}

	private static Direction ParseDirection(string direction)
	{
		return direction.ToUpper() switch
		{
			"U" => Direction.Up,
			"D" => Direction.Down,
			"L" => Direction.Left,
			"R" => Direction.Right,
			_ => throw new NotImplementedException(),
		};
	}
}
