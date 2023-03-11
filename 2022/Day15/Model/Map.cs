using Helpers;

namespace Day15.Model;

internal class Map : IMap
{
	private INode RootNode;
	private (int, int) Displacement;
	private static readonly Coordinate Zero = new Coordinate(0, 0);

	public Map(INode rootNode, Coordinate lowerCorner)
	{
		RootNode = rootNode;
		Displacement = lowerCorner.GetVectorTo(Zero);
	}

	public bool HasContent(Coordinate coordinate)
	{
		var relativeCoordinate = coordinate.AddVector(Displacement);
		return RootNode.HasContent(relativeCoordinate);
	}
}
