﻿using System.Diagnostics.CodeAnalysis;
using Helpers;

namespace Day15.Model;

internal class UncoveredNode : INode
{
	private static Dictionary<int, UncoveredNode> nodes = new();
	public static UncoveredNode GetNode(int sideLength)
	{
		if(nodes.TryGetValue(sideLength, out UncoveredNode? node))
			return node;

		node = new(sideLength);
		nodes[sideLength] = node;
		return node;
	}

	private int Sidelength;

	public UncoveredNode(int sideLength)
	{
		Sidelength = sideLength;
	}

	public INode Combine(INode other)
	{
		return other;
	}

	public bool HasContent(Coordinate coordinate)
	{
		return false;
	}

	public int CountRowCowerage(int row)
	{
		return 0;
	}

	public bool TryGetUncoveredSpot(Coordinate min, Coordinate max, [NotNullWhen(true)] out Coordinate? spot)
	{
		spot = new(0, 0);
		return true;
	}
}
