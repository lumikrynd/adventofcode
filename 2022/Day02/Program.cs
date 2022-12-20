namespace Day02;

internal partial class Program
{
	static void Main(string[] args)
	{
		Part1(Example);

		Console.WriteLine();
		Console.WriteLine();

		Part1(Input);

		Console.WriteLine();
		Console.WriteLine();

		Part2(Example);

		Console.WriteLine();
		Console.WriteLine();

		Part2(Input);
	}

	private static void Part1(string input)
	{
		var games = Parser.ParseInput(input);

		var totalScore = games.Sum(g => g.Score());

		Console.Write($"Final Score: {totalScore}");
	}

	private static void Part2(string input)
	{
		var games = Parser.ParseInputV2(input);

		var totalScore = games.Sum(g => g.Score());

		Console.Write($"Final Score: {totalScore}");
	}
}
