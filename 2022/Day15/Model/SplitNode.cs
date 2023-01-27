namespace Day15.Model;

internal class SplitNode : INode
{
	public INode UpperLeft { get; set; } = EmptyNode.Instance;
	public INode UpperRight { get; set; } = EmptyNode.Instance;
	public INode LowerLeft { get; set; } = EmptyNode.Instance;
	public INode LowerRight { get; set; } = EmptyNode.Instance;

	public int SideLengthPowerOfTwo { get; }

	public SplitNode(int sideLengthPowerOfTwo)
	{
		if (sideLengthPowerOfTwo <= 1)
			throw new Exception();

		SideLengthPowerOfTwo = sideLengthPowerOfTwo;
	}

	public INode Combine(INode other)
	{
		if(other is not SplitNode otherSplit)
		{
			return other.Combine(this);
		}

		var result = new SplitNode(SideLengthPowerOfTwo)
		{
			UpperLeft = UpperLeft.Combine(otherSplit.UpperLeft),
			UpperRight = UpperRight.Combine(otherSplit.UpperRight),
			LowerLeft = LowerLeft.Combine(otherSplit.LowerLeft),
			LowerRight = LowerRight.Combine(otherSplit.LowerRight),
		};

		if (result.UpperLeft is FullNode &&
			result.UpperRight is FullNode &&
			result.LowerLeft is FullNode &&
			result.LowerRight is FullNode)
			return FullNode.Instance;

		return result;
	}

	public bool HasContent(int x, int y)
	{
		throw new NotImplementedException();
	}
}
