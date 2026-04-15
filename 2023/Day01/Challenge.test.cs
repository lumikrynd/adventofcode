using NUnit.Framework;

namespace Y2023.Day01;

public class ChallengeTest
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day01/Example.txt");
	static IEnumerable<string> Example2Input => File.ReadLines(@"Input/Day01/Example2.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo("142"));
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(Example2Input);
		Assert.That(result, Is.EqualTo("281"));
	}

	private string Part1(IEnumerable<string> input)
	{
		var challenge = new Challenge(input);
		return challenge.Part1();
	}

	private string Part2(IEnumerable<string> input)
	{
		var challenge = new Challenge(input);
		return challenge.Part2();
	}
}