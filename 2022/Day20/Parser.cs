namespace Day20;

internal class Parser : IDisposable
{
	public static List<int> ParseInput(string input)
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

	private List<int> ParseInput()
	{
		var numbers = new List<int>();

		while(Stream.Peek() != -1)
		{
			var line = Stream.ReadLine() ?? "";
			numbers.Add(int.Parse(line));
		}

		return numbers;
	}
}
