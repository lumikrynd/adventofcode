namespace Y2022.Day13.Model;

internal class Pair
{
	public IValue Left { get; init; }
	public IValue Right { get; init; }

	public Pair(IValue left, IValue right)
	{
		Left = left;
		Right = right;
	}

	public override string ToString()
	{
		return $"{Left}\n{Right}";
	}
}