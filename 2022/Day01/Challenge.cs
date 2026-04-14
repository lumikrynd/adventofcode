using Helpers;
using NUnit.Framework;

namespace Y2022.Day01;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("24000"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("45000"));
	}
}

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