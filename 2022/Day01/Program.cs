using Helpers;

namespace Y2022.Day01;

internal partial class Program
{
	static void Main(string[] args)
	{
		var example = new Challenge(Example.Split('\n'));
		Console.WriteLine(example.Part1());
		Console.WriteLine(example.Part2());
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var elfs = Parser.ParseInput(input);

		var maxTotal = elfs.Max(s => s.Sum());

		return $"Sum: {maxTotal}";
	}

	public string Part2()
	{
		var elfs = Parser.ParseInput(input);

		var ordered = elfs.OrderByDescending(elf => elf.Sum());

		var sum = ordered.Take(3).Sum(elf => elf.Sum());

		return $"Sum: {sum}";
	}
}