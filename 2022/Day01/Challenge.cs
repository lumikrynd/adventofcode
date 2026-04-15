using Helpers;

namespace Y2022.Day01;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var elfs = Parser.ParseInput(input);

		var maxTotal = elfs.Max(s => s.Sum());
		return maxTotal.ToString();
	}

	public string Part2()
	{
		var elfs = Parser.ParseInput(input);

		var ordered = elfs.OrderByDescending(elf => elf.Sum());

		var sum = ordered.Take(3).Sum(elf => elf.Sum());
		return sum.ToString();
	}
}