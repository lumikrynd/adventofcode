using System.Diagnostics.CodeAnalysis;
using Helpers;

namespace Day15.Model;

internal class SplitNode : INode
{
	public INode[] Quadrants;
	private int SideLength;

	public SplitNode(int sideLength)
	{
		if (sideLength <= 1)
			throw new Exception();

		SideLength = sideLength;
		InitializeQuadrants();
	}

	[MemberNotNull(nameof(Quadrants))]
	private void InitializeQuadrants()
	{
		Quadrants = new INode[4];
		for(int i = 0; i < 4; i++)
			Quadrants[i] = UncoveredNode.Instance;
	}

	public INode UpperLeft
	{
		get => Quadrants[(int)Quadrant.UpperLeft];
		set => Quadrants[(int)Quadrant.UpperLeft] = value;
	}

	public INode UpperRight
	{
		get => Quadrants[(int)Quadrant.UpperRight];
		set => Quadrants[(int)Quadrant.UpperRight] = value;
	}

	public INode LowerLeft
	{
		get => Quadrants[(int)Quadrant.LowerLeft];
		set => Quadrants[(int)Quadrant.LowerLeft] = value;
	}

	public INode LowerRight
	{
		get => Quadrants[(int)Quadrant.LowerRight];
		set => Quadrants[(int)Quadrant.LowerRight] = value;
	}

	public INode Combine(INode other)
	{
		if(other is not SplitNode otherSplit)
			return other.Combine(this);

		var result = new SplitNode(SideLength)
		{
			UpperLeft = UpperLeft.Combine(otherSplit.UpperLeft),
			UpperRight = UpperRight.Combine(otherSplit.UpperRight),
			LowerLeft = LowerLeft.Combine(otherSplit.LowerLeft),
			LowerRight = LowerRight.Combine(otherSplit.LowerRight),
		};

		if(Quadrants.All(q => q is CoveredNode))
			return CoveredNode.Instance;

		return result;
	}

	public bool HasContent(Coordinate coordinate)
	{
		if(IsOutside(coordinate.X) || IsOutside(coordinate.Y))
			return false;

		int half = SideLength / 2;
		int quadrant = coordinate.X / half;
		quadrant += (coordinate.Y / half) * 2;

		int x = coordinate.X % half;
		int y = coordinate.Y % half;
		var newCoordinate = new Coordinate(x, y);

		return Quadrants[quadrant].HasContent(newCoordinate);
	}

	public bool IsOutside(int xy) =>
		xy < 0 || xy >= SideLength;

	public enum Quadrant
	{
		UpperLeft = 0,
		UpperRight = 1,
		LowerLeft = 2,
		LowerRight = 3,
	}
}
