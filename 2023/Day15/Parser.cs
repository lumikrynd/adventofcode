using Y2023.Day15.Models;

namespace Y2023.Day15;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static List<Step> Parse(IEnumerable<string> input)
	{
		var result = new List<string>();
		foreach(var line in input)
		{
			var steps = line.Split(',', splitOptions);
			result.AddRange(steps);
		}
		return result.Select(ParseStep)
			.ToList();
	}

	private static Step ParseStep(string input)
	{
		int splitPoint = input.IndexOfAny(['-', '=']);

		var label = input.Substring(0, splitPoint);
		var rest = input.Substring(splitPoint);

		var operation = ParseOperation(rest[0]);
		var focalLength = 0;
		if(operation == Operation.Set)
			focalLength = int.Parse(rest.Substring(1));

		return new()
		{
			FullStep = input,
			Label = label,
			Operation = operation,
			FocalLength = focalLength,
		};
	}

	private static Operation ParseOperation(char v) => v switch
	{
		'-' => Operation.Remove,
		'=' => Operation.Set,
		_ => throw new NotImplementedException(),
	};
}