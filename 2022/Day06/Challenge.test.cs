using FluentAssertions;
using NUnit.Framework;

namespace Y2022.Day06;

public class ChallengeTest
{
	public static IEnumerable<object[]> ExampleData()
	{
		yield return new object[] { "mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7, 19 };
		yield return new object[] { "bvwbjplbgvbhsrlpgdmjqwftvncz", 5, 23 };
		yield return new object[] { "nppdvjthqldpwncqszvftbrmjlhg", 6, 23 };
		yield return new object[] { "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10, 29 };
		yield return new object[] { "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11, 26 };
	}

	[TestCaseSource(nameof(ExampleData))]
	public void Part1_Example(string input, int expectedResult, int _)
	{
		var challenge = new Challenge(input);
		var result = challenge.Part1();
		result.Should().Be(expectedResult.ToString());
	}

	[TestCaseSource(nameof(ExampleData))]
	public void Part2_Example(string input, int _, int expectedResult)
	{
		var challenge = new Challenge(input);
		var result = challenge.Part2();
		result.Should().Be(expectedResult.ToString());
	}
}