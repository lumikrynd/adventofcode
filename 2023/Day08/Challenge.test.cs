using NUnit.Framework;

namespace Y2023.Day08;

public class ChallengeTest
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day08/Example.txt");
	static IEnumerable<string> Example2Input => File.ReadLines(@"Input/Day08/Example2.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("6"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(Example2Input);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("6"));
	}
}