namespace Day09;

internal class Move
{
	public Direction Direction { get; }
	public int Steps { get; }

	public Move(Direction direction, int steps)
	{
		Direction = direction;
		Steps = steps;
	}
}
