using Helpers;

namespace Day15.Model;

internal class Map
{
	private INode RootNode;
	private Coordinate LowerCorner;

	public Map(INode rootNode, Coordinate lowerCorner)
	{
		RootNode = rootNode;
		LowerCorner= lowerCorner;
	}

	public bool HasContent(Coordinate coordinate)
	{
		(int xs, int ys) = ((int, int))LowerCorner;
		var relativeCoordinate = coordinate.AddVector((-xs, -ys));
		return RootNode.HasContent(relativeCoordinate);
	}
}
