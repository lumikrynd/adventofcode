namespace Day15.Model;

internal class FullNode : INode
{
	public static FullNode Instance { get; } = new();

	public int SideLengthPowerOfTwo => 0;

	private FullNode()
	{
	}

	public INode Combine(INode other)
	{
		return this;
	}

	public bool HasContent(int x, int y)
	{
		return true;
	}
}
