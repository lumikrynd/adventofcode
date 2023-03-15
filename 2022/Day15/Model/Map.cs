using System.Diagnostics.CodeAnalysis;
using System.Text;
using Helpers;

namespace Day15.Model;

internal class Map : IMap
{
	private INode RootNode;
	private (int Xs, int Ys) Displacement;
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

	public int CountRowCowerage(int row)
	{
		var relativeRow = row + Displacement.Ys;
		return RootNode.CountRowCowerage(relativeRow);
	}

	public bool TryGetUncoveredSpot(Coordinate min, Coordinate max, [NotNullWhen(true)] out Coordinate? spot)
	{
		var relativeMin = min.AddVector(Displacement);
		var relativeMax = max.AddVector(Displacement);
		if(!RootNode.TryGetUncoveredSpot(relativeMin, relativeMax, out var relativeSpot))
		{
			spot = null;
			return false;
		}

		spot = relativeSpot.SubtractVector(Displacement);
		return true;
	}

	public string GetMapArea(Coordinate lower, Coordinate upper)
	{
		var sb = new StringBuilder();
		for(int y = lower.Y; y <= upper.Y; y++)
		{
			for(int x = lower.X; x <= upper.X; x++)
			{
				char c = HasContent(new(x, y)) ? '#' : '.';
				sb.Append(c);
			}
			sb.AppendLine();
		}
		return sb.ToString();
	}
}
