using Day15.Parsing;

namespace Day15;

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

	static void Part1(string input)
	{
		Parser.ParseInput(input);
	}

	static void Part2(string input)
	{

	}
}