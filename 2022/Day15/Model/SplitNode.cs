using System.Diagnostics.CodeAnalysis;
using Helpers;

namespace Day15.Model;

internal class SplitNode : INode
{
	public INode[] Quadrants;
	private int SideLength;
	private int HalfSide => SideLength / 2;

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
			Quadrants[i] = UncoveredNode.GetNode(SideLength / 2);
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
			return CoveredNode.GetNode(SideLength);

		return result;
	}

	public bool HasContent(Coordinate coordinate)
	{
		if(IsOutside(coordinate.X) || IsOutside(coordinate.Y))
			return false;

		int quadrant = GetCoordinateQuadrant(coordinate);
		int x = coordinate.X % HalfSide;
		int y = coordinate.Y % HalfSide;
		var newCoordinate = new Coordinate(x, y);

		return Quadrants[quadrant].HasContent(newCoordinate);
	}

	private int GetCoordinateQuadrant(Coordinate coordinate)
	{
		int quadrant = coordinate.X / HalfSide;
		quadrant += (coordinate.Y / HalfSide) * 2;
		return quadrant;
	}

	public bool IsOutside(int xy) =>
		xy < 0 || xy >= SideLength;

	public int CountRowCowerage(int row)
	{
		int quadrantModifier = (row / HalfSide) * 2;
		int newRelativeRow = row % HalfSide;
		int sum = 0;
		sum += Quadrants[quadrantModifier].CountRowCowerage(newRelativeRow);
		sum += Quadrants[quadrantModifier + 1].CountRowCowerage(newRelativeRow);
		return sum;
	}

	public bool TryGetUncoveredSpot(Coordinate min, Coordinate max, [NotNullWhen(true)] out Coordinate? spot)
	{
		bool result = TryGetUncoveredFromQuadrant(min, max, Quadrant.UpperLeft, out spot);
		result = result || TryGetUncoveredFromQuadrant(min, max, Quadrant.UpperRight, out spot);
		result = result || TryGetUncoveredFromQuadrant(min, max, Quadrant.LowerLeft, out spot);
		result = result || TryGetUncoveredFromQuadrant(min, max, Quadrant.LowerRight, out spot);
		return result;
	}

	bool TryGetUncoveredFromQuadrant(Coordinate min, Coordinate max, Quadrant quadrant, [NotNullWhen(true)] out Coordinate? spot)
	{
		if(!ExistInQuadrant(min, max, quadrant))
		{
			spot = null;
			return false;
		}

		(var newMin, var newMax) = GetNewMinMax(min, max, quadrant);
		var node = Quadrants[(int)quadrant];

		var result = node.TryGetUncoveredSpot(newMin, newMax, out var displaceResult);
		spot = result ? UndoDisplacementForResult(displaceResult!, quadrant) : null; //Annoys me the compiler forces me to use ! here...
		return result;
	}

	bool ExistInQuadrant(Coordinate min, Coordinate max, Quadrant quadrant)
	{
		Coordinate point = quadrant switch
		{
			Quadrant.UpperLeft => min,
			Quadrant.UpperRight => new(max.X, min.Y),
			Quadrant.LowerLeft => new(min.X, max.Y),
			Quadrant.LowerRight => max,
			_ => throw new NotImplementedException(),
		};
		var actualQuadrant = GetCoordinateQuadrant(point);
		return actualQuadrant == (int)quadrant;
	}

	Coordinate? quadrantMax;
	Coordinate QuadrantMax => quadrantMax ??= new(HalfSide - 1, HalfSide - 1);
	static Coordinate Zero { get; } = new(0, 0);

	(Coordinate min, Coordinate max) GetNewMinMax(Coordinate min, Coordinate max, Quadrant quadrant)
	{
		var displaceMent = GetQadrantDisplacement(quadrant);
		var newMin = min.SubtractVector(displaceMent);
		var newMax = max.SubtractVector(displaceMent);

		newMin = MaxDimmension(newMin, Zero);
		newMax = MinDimmension(newMax, QuadrantMax);
		return (newMin, newMax);
	}

	Coordinate UndoDisplacementForResult(Coordinate result, Quadrant quadrant)
	{
		var displaceMent = GetQadrantDisplacement(quadrant);
		return result.AddVector(displaceMent);
	}

	(int xs, int ys) GetQadrantDisplacement(Quadrant quadrant)
	{
		return quadrant switch
		{
			Quadrant.UpperLeft => (0, 0),
			Quadrant.UpperRight => (HalfSide, 0),
			Quadrant.LowerLeft => (0, HalfSide),
			Quadrant.LowerRight => (HalfSide, HalfSide),
			_ => throw new NotImplementedException(),
		};
	}

	Coordinate MinDimmension(Coordinate a, Coordinate b) => MaxMin(Math.Min, a, b);
	Coordinate MaxDimmension(Coordinate a, Coordinate b) => MaxMin(Math.Max, a, b);

	Coordinate MaxMin(Func<int, int, int> func, Coordinate a, Coordinate b)
	{
		var x = func(a.X, b.X);
		var y = func(a.Y, b.Y);
		return new(x, y);
	}

	public enum Quadrant
	{
		UpperLeft = 0,
		UpperRight = 1,
		LowerLeft = 2,
		LowerRight = 3,
	}
}
