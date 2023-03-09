using Helpers;

namespace Day15.Model;

internal class CoveredNode : INode
{
	public static CoveredNode Instance { get; } = new();

	public int SideLengthPowerOfTwo => 0;

	private CoveredNode()
	{
	}

	public INode Combine(INode other)
	{
		return this;
	}

	public bool HasContent(Coordinate coordinate)
	{
		return true;
	}
}
