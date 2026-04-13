using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day06;

public class Test
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

public class Challenge(params IEnumerable<string> data) : ISolver
{
	public string Part1()
	{
		return PuzzleSolution(data.Single(), 4).ToString();
	}

	public string Part2()
	{
		return PuzzleSolution(data.Single(), 14).ToString();
	}

	private static int PuzzleSolution(string input, int packageSize)
	{
		CircularStorage storage = new(packageSize);
		int count = packageSize - 1;
		foreach(char c in input.Take(packageSize - 1))
		{
			storage.AddValue(c);
		}

		foreach(char c in input.Skip(packageSize - 1))
		{
			count++;
			storage.AddValue(c);
			if(storage.Distinct().Count() == packageSize)
				break;
		}

		return count;
	}
}