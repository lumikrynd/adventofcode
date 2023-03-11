using System.Diagnostics.CodeAnalysis;
using Day15.Model;
using Helpers;

namespace Day15.Parsing;

internal class MapCreater
{
	List<SensorResponse> Coordinates;
	Coordinate LowerBound;
	Coordinate UpperBound;
	public MapCreater(List<SensorResponse> coordinates)
	{
		Coordinates = coordinates;
		CalculateBounds();
	}

	public static Map CreateMap(List<SensorResponse> coordinates)
	{
		var creater = new MapCreater(coordinates);
		return creater.GetMap();
	}

	[MemberNotNull(nameof(LowerBound), nameof(UpperBound))]
	private void CalculateBounds()
	{
		int xMin = int.MaxValue, xMax = int.MinValue;
		int yMin = int.MaxValue, yMax = int.MinValue;

		foreach(var coord in Coordinates)
		{
			var sensor = coord.Sensor;
			var dist = sensor.ManhattenDistance(coord.Beacon);

			xMin = Math.Min(xMin, sensor.X - dist);
			yMin = Math.Min(yMin, sensor.Y - dist);
			xMax = Math.Max(xMax, sensor.X + dist);
			yMax = Math.Max(yMax, sensor.Y + dist);
		}
		LowerBound = new(xMin, yMin);
		UpperBound = new(xMax, yMax);
	}

	Map? Map;
	public Map GetMap()
	{
		if(Map is null)
			CreateMap();
		return Map;
	}

	[MemberNotNull(nameof(Map))]
	private void CreateMap()
	{
		int dimmensionSize = DimensionsSize();
		INode main = UncoveredNode.GetNode(dimmensionSize);

		foreach(var coord in Coordinates)
		{
			var node = CreateNodeFromCoordinates(coord, dimmensionSize);
			main = main.Combine(node);
		}

		Map = new(main, LowerBound);
	}

	private int DimensionsSize()
	{
		var xRangeSize = UpperBound.X - LowerBound.X + 1;
		var yRangeSize = UpperBound.Y - LowerBound.Y + 1;
		var neededSize = Math.Max(xRangeSize, yRangeSize);
		var roundedUp = Bits.RoundUpPowerOfTwo(neededSize);
		return roundedUp;
	}

	private INode CreateNodeFromCoordinates(SensorResponse input, int dimmensionSize)
	{
		var distance = input.Sensor.ManhattenDistance(input.Beacon);
		var relativePosition = LowerBound.GetVectorTo(input.Sensor);
		return NodeCreater.CreateNode(new(relativePosition), distance, dimmensionSize);
	}
}
