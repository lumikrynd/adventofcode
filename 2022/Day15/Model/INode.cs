using Helpers;

namespace Day15.Model;

internal interface INode
{
	bool HasContent(Coordinate coordinate);
	public INode Combine(INode other);
}
