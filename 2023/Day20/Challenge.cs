using Helpers;
using NUnit.Framework;

namespace Y2023.Day20;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Example2.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("32000000"));
	}

	[Test]
	public void Part1_Example2()
	{
		var challenge = new Challenge(ExampleInput2);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("11687500"));
	}

	[Test, Ignore("Not done")]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("42"));
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var modules = Parser.Parse(input);
		var emulator = new Emulator(modules);

		emulator.PushButton(1000);

		var counts = emulator.GetCounts();
		var result = 1L * counts.Low * counts.High;

		return result.ToString();
	}

	public string Part2()
	{
		var modules = Parser.Parse(input);
		var emulator = new Emulator(modules);

		return "";
	}
}