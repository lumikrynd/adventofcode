using Y2023.Day20.Models;

namespace Y2023.Day20;

public class Parser
{
	private static readonly StringSplitOptions splitOptions = StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries;

	public static List<Module> Parse(IEnumerable<string> lines)
	{
		return lines
			.Select(Parse)
			.ToList();
	}

	private static Module Parse(string line)
	{
		var parts = line.Split("->", splitOptions);

		var name = ParseName(parts[0]);
		var type = ParseType(line[0]);
		var destinations = ParseDestinations(parts[1]);

		return new(name, type, destinations);
	}

	private static string ParseName(string v)
		=> v.Trim(' ', '%', '&');

	private static ModuleType ParseType(char v) => v switch
	{
		'%' => ModuleType.FlipFlop,
		'&' => ModuleType.Conjunction,
		_ => ModuleType.None,
	};

	private static List<string> ParseDestinations(string v)
		=> v.Split(',', splitOptions).ToList();
}