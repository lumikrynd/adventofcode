using Helpers;

namespace Day15.Model;

internal class CoveredNode : INode
{
	private static Dictionary<int, CoveredNode> nodes = new();
	public static CoveredNode GetNode(int sideLength)
	{
		if(nodes.TryGetValue(sideLength, out CoveredNode? node))
			return node;

		node = new(sideLength);
		nodes[sideLength] = node;
		return node;
	}

	private int Sidelength;

	public CoveredNode(int sideLength)
	{
		Sidelength = sideLength;
	}

	public INode Combine(INode other)
	{
		return this;
	}

	public bool HasContent(Coordinate coordinate)
	{
		return true;
	}

	public int CountRowCowerage(int row)
	{
		return Sidelength;
	}
}
