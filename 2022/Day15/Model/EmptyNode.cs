namespace Day15.Model;

internal class EmptyNode : INode
{
	public static EmptyNode Instance { get; set; } = new();

	public int SideLengthPowerOfTwo => 0;

	private EmptyNode()
	{
	}

	public INode Combine(INode other)
	{
		return other;
	}

	public bool HasContent(int x, int y)
	{
		return false;
	}
}
