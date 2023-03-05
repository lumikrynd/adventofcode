using System.Text.RegularExpressions;
using Day15.Model;
using Helpers;

namespace Day15.Parsing;

internal class Parser
{
	public static List<SensorResponse> ParseInput(IEnumerable<string> input)
	{
		var responses = new List<SensorResponse>();
		foreach(var line in input)
		{
			SensorResponse response = ParseResponse(line);
			responses.Add(response);
		}
		return responses;
	}

	private static SensorResponse ParseResponse(string line)
	{
		var match = Regex.Match(line, Pattern) ?? throw new Exception($"{line} didn't match pattern");
		var sensor = new Coordinate(ToInt(match, 1), ToInt(match, 2));
		var beacon = new Coordinate(ToInt(match, 3), ToInt(match, 4));
		return new SensorResponse(sensor, beacon);
	}

	private static int ToInt(Match match, int index)
	{
		return int.Parse(match.Groups[index].Value);
	}

	private static string Pattern => $"^{NextNumber}{NextNumber}{NextNumber}{NextNumber}$";
	private static string NextNumber = @"[^-0-9]*([-0-9]*)";

	private static Wrapper CreateDataStructure(List<SensorResponse> coordinates)
	{
		var (min, max) = CalculateMapBounds(coordinates);

		var xRange = max.X - min.X + 1;
		var yRange = max.Y - min.Y + 1;
		var neededSize = Math.Max(xRange, yRange);
		var toPow2 = Bits.LogBase2(Bits.RoundUpPowerOfTwo(neededSize));

		INode main = EmptyNode.Instance;

		foreach(var coord in coordinates)
		{
			var node = CreateNodeFromCoordinates(coord, toPow2, min);
			main = main.Combine(node);
		}

		return new Wrapper(main, min);
	}

	private static INode CreateNodeFromCoordinates(SensorResponse input, int pow2size, Coordinate lowerBound)
	{
		var distance = input.Sensor.ManhattenDistance(input.Beacon);
		var relativePosition = lowerBound.GetVectorTo(input.Sensor);

		return GenerateNode(relativePosition, distance, pow2size);
	}

	/// <summary>
	/// All inputs relative to current node being generated
	/// </summary>
	private static INode GenerateNode((int x, int y) center, int radius, int pow2size)
	{
		int upperLimit = (1 << pow2size) - 1;

		if (NodeIsOutsideRadius(center, radius, upperLimit))
			return EmptyNode.Instance;

		if (NodeIsWithinRadius(center, radius, upperLimit))
			return FullNode.Instance;

		int halfSize = (1 << (pow2size - 1));

		return new SplitNode(pow2size)
		{
			UpperLeft = GenerateNode(center, radius, pow2size - 1),
			UpperRight = GenerateNode((center.x - halfSize, center.y), radius, pow2size - 1),
			LowerLeft = GenerateNode((center.x, center.y - halfSize), radius, pow2size - 1),
			LowerRight = GenerateNode((center.x - halfSize, center.y - halfSize), radius, pow2size - 1),
		};
	}

	private static bool NodeIsOutsideRadius((int x, int y) center, int radius, int upperLimit)
	{
		return
			center.ManhattenDistance((0, 0)) > radius &&
			center.ManhattenDistance((0, upperLimit)) > radius &&
			center.ManhattenDistance((upperLimit, 0)) > radius &&
			center.ManhattenDistance((upperLimit, upperLimit)) > radius;
	}

	private static bool NodeIsWithinRadius((int x, int y) center, int radius, int upperLimit)
	{
		return
			center.ManhattenDistance((0, 0)) <= radius &&
			center.ManhattenDistance((0, upperLimit)) <= radius &&
			center.ManhattenDistance((upperLimit, 0)) <= radius &&
			center.ManhattenDistance((upperLimit, upperLimit)) <= radius;
	}

	private static (Coordinate min, Coordinate max) CalculateMapBounds(List<SensorResponse> coordinates)
	{
		int xMin = int.MaxValue, xMax = int.MinValue;
		int yMin = int.MaxValue, yMax = int.MinValue;

		foreach (var coord in coordinates)
		{
			var sensor = coord.Sensor;
			var dist = sensor.ManhattenDistance(coord.Beacon);

			xMin = Math.Min(xMin, sensor.X - dist);
			yMin = Math.Min(yMin, sensor.Y - dist);
			xMax = Math.Max(xMax, sensor.X + dist);
			yMax = Math.Max(yMax, sensor.Y + dist);
		}

		return (new(xMin, yMin), new(xMax, yMax));
	}
}
