namespace Day02;

internal class Parser : IDisposable
{
	public static List<Game> ParseInput(string input)
	{
		using var parser = new Parser(input);

		return parser.ParseInput();
	}

	public static List<Game> ParseInputV2(string input)
	{
		using var parser = new Parser(input);

		return parser.ParseInputV2();
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

	private List<Game> ParseInput()
	{
		var games = new List<Game>();

		while(Stream.Peek() != -1)
		{
			var line = Stream.ReadLine() ?? "";
			var game = new Game(
				ToMove(line[2]),
				ToMove(line[0])
			);

			games.Add(game);
		}

		return games;
	}

	private List<Game> ParseInputV2()
	{
		var games = new List<Game>();

		while(Stream.Peek() != -1)
		{
			var line = Stream.ReadLine() ?? "";
			var game = new Game(
				ToResult(line[2]),
				ToMove(line[0])
			);

			games.Add(game);
		}

		return games;
	}

	private Move ToMove(char c)
	{
		return c switch
		{
			'A' => Move.Rock,
			'B' => Move.Paper,
			'C' => Move.Scissor,
			'X' => Move.Rock,
			'Y' => Move.Paper,
			'Z' => Move.Scissor,
			_ => throw new Exception()
		};
	}

	private Result ToResult(char c)
	{
		return c switch
		{
			'X' => Result.Loose,
			'Y' => Result.Draw,
			'Z' => Result.Win,
			_ => throw new Exception()
		};
	}
}
