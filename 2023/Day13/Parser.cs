using Helpers;

namespace Y2023.Day13;

public class Parser : IDisposable
{
	public static List<char[,]> Parse(IEnumerable<string> input)
	{
		using var parser = new Parser(input);
		parser.Parse();
		return parser.Patterns;
	}

	readonly EnumeratorWrapper<string> Input;
	public List<char[,]> Patterns { get; private init; } = [];

	private Parser(IEnumerable<string> input)
	{
		Input = EnumeratorWrapper<string>.WithInitializedCurrent(input);
	}

	public void Parse()
	{
		while(Input.HasNext())
		{
			var lines = GetNextPatternLines();
			Patterns.Add(ToMultidimensionalArray(lines));
		}
	}

	private List<string> GetNextPatternLines()
	{
		List<string> lines = [];
		do
		{
			lines.Add(Input.Current);
		}
		while(Input.TryGetNext(out var s) && !string.IsNullOrWhiteSpace(s));
		Input.TryGetNext(out _);

		return lines;
	}

	private static char[,] ToMultidimensionalArray(List<string> lines)
	{
		int yl = lines.Count;
		int xl = lines.First().Length;
		char[,] arr = new char[xl, yl];

		for(int y = 0; y < yl; y++)
		{
			for(int x = 0; x < xl; x++)
			{
				arr[x, y] = lines[y][x];
			}
		}

		return arr;
	}

	public void Dispose()
	{
		Input.Dispose();
	}
}
