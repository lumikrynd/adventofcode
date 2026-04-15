using NUnit.Framework;

namespace Y2023.Day20;

public class ChallengeTest
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day20/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Day20/Example2.txt");

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