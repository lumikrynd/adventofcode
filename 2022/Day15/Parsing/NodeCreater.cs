using System.Diagnostics.CodeAnalysis;
using Day15.Model;
using Helpers;

namespace Day15.Parsing;

internal class NodeCreater
{
	readonly Coordinate Sensor;
	readonly int Radius;
	readonly int DimmensionSize;
	Coordinate[] Corners;

	/// <param name="sensor">Coordinate relative to lower corner of generated node</param>
	public NodeCreater(Coordinate sensor, int radius, int dimmensionSize)
	{
		Sensor = sensor;
		Radius = radius;
		DimmensionSize = dimmensionSize;
		InitializeCorners(DimmensionSize - 1);
	}

	[MemberNotNull(nameof(Corners))]
	private void InitializeCorners(int upperLimit)
	{
		Corners = new Coordinate[]
		{
			new(0, 0),
			new(0, upperLimit),
			new(upperLimit, 0),
			new(upperLimit, upperLimit),
		};
	}

	/// <param name="sensor">Coordinate relative to lower corner of generated node</param>
	public static INode CreateNode(Coordinate sensor, int radius, int dimmensionSize)
	{
		var creator = new NodeCreater(sensor, radius, dimmensionSize);
		return creator.CreateNode();
	}

	public INode CreateNode()
	{
		if(InsideOrOutside(out INode? node))
			return node;
		return CreateSplitNode();
	}

	private INode CreateSplitNode()
	{
		var nextSize = DimmensionSize / 2;
		return new SplitNode(DimmensionSize)
		{
			UpperLeft = CreateNode(Sensor, Radius, nextSize),
			UpperRight = CreateNode(Sensor.SubtractVector((nextSize, 0)), Radius, nextSize),
			LowerLeft = CreateNode(Sensor.SubtractVector((0, nextSize)), Radius, nextSize),
			LowerRight = CreateNode(Sensor.SubtractVector((nextSize, nextSize)), Radius, nextSize),
		};
	}

	private bool InsideOrOutside([NotNullWhen(true)] out INode? node)
	{
		node = null;
		int upperLimit = DimmensionSize - 1;

		if (SensorRadiusOutsideNode(upperLimit))
			node = UncoveredNode.GetNode(DimmensionSize);

		if (SensorRadiusContainsNode(upperLimit))
			node = CoveredNode.GetNode(DimmensionSize);

		return node is not null;
	}

	private bool SensorRadiusOutsideNode(int upperLimit)
	{
		var maxCornerDistance = (upperLimit * 2) + Radius;
		return Corners.Any(c => c.ManhattenDistance(Sensor) > maxCornerDistance);
	}

	private bool SensorRadiusContainsNode(int upperLimit)
	{
		return Corners.All(c => c.ManhattenDistance(Sensor) <= Radius);
	}
}
