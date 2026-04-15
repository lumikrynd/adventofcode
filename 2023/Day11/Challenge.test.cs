using NUnit.Framework;

namespace Y2023.Day11;

public class ChallengeTest
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day11/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("374"));
	}

	[Test]
	public void Part2_Example_Mult10()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1and2(10);
		Assert.That(result, Is.EqualTo(1030));
	}

	[Test]
	public void Part2_Example_Mult100()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1and2(100);
		Assert.That(result, Is.EqualTo(8410));
	}
}