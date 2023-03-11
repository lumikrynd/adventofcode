using System.Text.RegularExpressions;
using Helpers;

namespace Day15.Parsing;

internal class SensorResponseParser
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
	private static readonly string NextNumber = @"[^-0-9]*([-0-9]*)";
}
