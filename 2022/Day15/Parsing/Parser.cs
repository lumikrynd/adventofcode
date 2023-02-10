using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Day15.Model;
using Helpers;

namespace Day15.Parsing;

internal class Parser
{
	public static Wrapper ParseInput(string input)
	{
		var coordinates = ParseCoordinates(input);
		return CreateDataStructure(coordinates);
	}

	private static Wrapper CreateDataStructure(List<ParsedLine> coordinates)
	{
		var (min, max) = CalculateMapBounds(coordinates);

		var xRange = max.x - min.x + 1;
		var yRange = max.y - min.y + 1;
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

	private static INode CreateNodeFromCoordinates(ParsedLine input, int pow2size, (int x, int y) lowerBound)
	{
		var distance = CoordinateHelpers.ManhattenDistance(input.SensorPosition, input.BeaconPosition);
		var relativePosition = input.SensorPosition.RelativeTo(lowerBound);

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

	private static ((int x, int y) min, (int x, int y) max) CalculateMapBounds(List<ParsedLine> coordinates)
	{
		int xMin = int.MaxValue, xMax = int.MinValue;
		int yMin = int.MaxValue, yMax = int.MinValue;

		foreach (var coord in coordinates)
		{
			var sensor = coord.SensorPosition;
			var dist = CoordinateHelpers.ManhattenDistance(sensor, coord.BeaconPosition);

			xMin = Math.Min(xMin, sensor.x - dist);
			yMin = Math.Min(yMin, sensor.y - dist);
			xMax = Math.Min(xMax, sensor.x + dist);
			yMax = Math.Min(yMax, sensor.y + dist);
		}

		return ((xMin, yMin), (xMax, yMax));
	}

	private static List<ParsedLine> ParseCoordinates(string input)
	{
		var matches = Regex.Matches(input, AltPattern, RegexOptions.Multiline);

		if (matches.Count is not (14 or 38))
			throw new Exception($"{matches.Count}");

		var coordinates = new List<ParsedLine>();

		foreach(Match match in matches)
		{
			var coord = new ParsedLine()
			{
				SensorPosition = (ToInt(match, 1), ToInt(match, 2)),
				BeaconPosition = (ToInt(match, 3), ToInt(match, 4)),
			};

			coordinates.Add(coord);
		}

		return coordinates;
	}

	private static int ToInt(Match match, int index)
	{
		return int.Parse(match.Groups[index].Value);
	}

	private static string AltPattern = @"^Sensor at x=([-0-9]*), y=([-0-9]*): closest beacon is at x=([-0-9]*), y=([-0-9]*)(.*)$";

	private static string Pattern => $"^{NextNumber}{NextNumber}{NextNumber}{NextNumber}$";

	private static string NextNumber = @"[^-0-9]*([-0-9]*)";
}
