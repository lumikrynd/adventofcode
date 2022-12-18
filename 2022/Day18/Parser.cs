using System.Text.RegularExpressions;

namespace Day18;

internal class Parser : IDisposable
{
	public static List<(int x, int y, int z)> ParseInput(string input)
	{
		using var parser = new Parser(input);

		return parser.ParseInput();
	}

	readonly StringReader Stream;

	private Parser(string input)
	{
		Stream = new StringReader(input);
	}

	public void Dispose()
	{
		Stream.Dispose();
	}

	public List<(int x, int y, int z)> ParseInput()
	{
		var result = new List<(int x, int y, int z)>();

		while(Stream.Peek() != -1)
		{
			var line = Stream.ReadLine() ?? "";
			result.Add(ParseLine(line));
		}

		return result;
	}

	private static (int x, int y, int z) ParseLine(string line)
	{
		var matches = Regex.Matches(line, @"^([0-9]+),([0-9]+),([0-9]+).*$", RegexOptions.Multiline);

		if (matches.Count != 1)
			throw new Exception();

		Match match = matches.First();

		return (GetValue(match, 1), GetValue(match, 2), GetValue(match, 3));
	}

	private static int GetValue(Match match, int index) => int.Parse(match.Groups[index].Value);
}
