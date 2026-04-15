using NUnit.Framework;

namespace Y2023.Day10;

public class ChallengeTest
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day10/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Day10/Example2.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("8"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput2);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("10"));
	}
}