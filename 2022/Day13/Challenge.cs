using Helpers;
using NUnit.Framework;

namespace Y2022.Day13;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day13/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("13"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("140"));
	}
}

public class Challenge(string input) : ISolver
{
	public Challenge(IEnumerable<string> input) : this(string.Join('\n', input))
	{
	}

	public string Part1()
	{
		var data = Parser.ParseInput(input);

		var sum = 0;
		for (int i = 0; i < data.Count; i++)
		{
			var current = data[i];
			var comparison = current.Left.CompareTo(current.Right);

			if (comparison <= 0)
				sum += i + 1;
		}

		return sum.ToString();
	}

	public string Part2()
	{
		var data = Parser.ParseInput(input);
		var extra = Parser.ParseInput(Extra).First();

		int leftCount = 0, rightCount = 0;

		if (extra.Left.CompareTo(extra.Right) >= 0)
			throw new Exception();

		foreach (var d in data)
		{
			if (extra.Left.CompareTo(d.Left) > 0) leftCount++;
			if (extra.Left.CompareTo(d.Right) > 0) leftCount++;

			if (extra.Right.CompareTo(d.Left) > 0) rightCount++;
			if (extra.Right.CompareTo(d.Right) > 0) rightCount++;
		}

		var result = (leftCount + 1) * (rightCount + 2);
		return result.ToString();
	}

	static readonly string Extra =
@"[[2]]
[[6]]";

}