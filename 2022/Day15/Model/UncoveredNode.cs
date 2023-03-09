using Helpers;

namespace Day15.Model;

internal class UncoveredNode : INode
{
	public static UncoveredNode Instance { get; set; } = new();

	public int SideLengthPowerOfTwo => 0;

	private UncoveredNode()
	{
	}

	public INode Combine(INode other)
	{
		return other;
	}

	public bool HasContent(Coordinate coordinate)
	{
		return false;
	}
}
