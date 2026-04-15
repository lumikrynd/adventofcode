using Helpers;
using NUnit.Framework;
using Y2023.Day13.Models;

namespace Y2023.Day13;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day13/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("405"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("400"));
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1() => Part1_2(0).ToString();
	public string Part2() => Part1_2(1).ToString();

	private long Part1_2(int smudges)
	{
		var patterns = Parser.Parse(input);

		long result = 0;
		foreach(var pattern in patterns)
		{
			var (dim, loc) = FindReflection(pattern, smudges);
			var multiply = dim == Dimension.Horizontal ? 100 : 1;
			result += loc * multiply;
		}

		return result;
	}

	private (Dimension, int) FindReflection(char[,] pattern, int smudges)
	{
		var xl = pattern.GetLength(0);
		var yl = pattern.GetLength(1);

		for(int x = 1; x < xl; x++)
		{
			if(FoundVerticalReflection(pattern, x, smudges))
				return (Dimension.Vertical, x);
		}

		for(int y = 1; y < yl; y++)
		{
			if(FoundHorizontalReflection(pattern, y, smudges))
				return (Dimension.Horizontal, y);
		}

		throw new NotImplementedException();
	}

	private static bool FoundVerticalReflection(char[,] pattern, int x, int smudges)
	{
		var xl = pattern.GetLength(0);
		var yl = pattern.GetLength(1);
		var dl = DLength(x, xl);
		int sCount = 0;
		for(int d = 0; d < dl; d++)
		{
			for(int y = 0; y < yl; y++)
			{
				if(pattern[x - 1 - d, y] != pattern[x + d, y])
					sCount++;
			}
		}

		return smudges == sCount;
	}

	private bool FoundHorizontalReflection(char[,] pattern, int y, int smudges)
	{
		var xl = pattern.GetLength(0);
		var yl = pattern.GetLength(1);
		var dl = DLength(y, yl);
		int sCount = 0;
		for(int d = 0; d < dl; d++)
		{
			for(int x = 0; x < xl; x++)
			{
				if(pattern[x, y - 1 - d] != pattern[x, y + d])
					sCount++;
			}
		}

		return smudges == sCount;
	}

	private static int DLength(int point, int length)
	{
		return Math.Min(point, length - point);
	}
}