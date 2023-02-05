namespace Day05.Models;

internal class Move
{
	public int Amount { get; }
	public int From { get; }
	public int To { get; }

	public Move(int amount, int from, int to)
	{
		Amount = amount;
		From = from;
		To = to;
	}
}
