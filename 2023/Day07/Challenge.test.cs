using NUnit.Framework;

namespace Y2023.Day07;

public class ChallengeTest
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day07/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("6440"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("5905"));
	}
}