using Helpers;
using NUnit.Framework;

namespace Y2023.Day11;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1and2(ExampleInput, 2);
		Assert.That(result, Is.EqualTo(374));
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1and2(PuzzleInput, 2);
	}

	[Test]
	public void Part2_Example_Mult10()
	{
		var result = Part1and2(ExampleInput, 10);
		Assert.That(result, Is.EqualTo(1030));
	}

	[Test]
	public void Part2_Example_Mult100()
	{
		var result = Part1and2(ExampleInput, 100);
		Assert.That(result, Is.EqualTo(8410));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part1and2(PuzzleInput, 1000000);
	}

	private long Part1and2(IEnumerable<string> input, int expansionMultiplier)
	{
		var stars = Parser.Parse(input);

		int[] rowsWithStars = SortedInstances(stars, s => s.Y);
		int[] columnsWithStars = SortedInstances(stars, s => s.X);

		long sum = 0;
		foreach(var (a, b)  in EveryPair(stars))
		{
			var distance = a.ManhattenDistance(b);
			var rowStarCount = FoundSubsetSize(rowsWithStars, a.Y, b.Y) - 1; // -1 because it include both start and end row
			var colStarCount = FoundSubsetSize(columnsWithStars, a.X, b.X) - 1;

			sum += distance + ((distance - rowStarCount - colStarCount) * (expansionMultiplier - 1));
		}

		Console.WriteLine($"Result: {sum}");
		return sum;
	}

	private int FoundSubsetSize(int[] set, int a, int b)
	{
		if(a > b)
			(a, b) = (b, a);

		int index1 = Array.BinarySearch(set, a);
		if(index1 < 0)
			index1 = ~index1;

		int inclusiveAdd = 1;
		int index2 = Array.BinarySearch(set, b);
		if(index2 < 0)
		{
			index2 = ~index2;
			inclusiveAdd = 0;
		}

		return index2 - index1 + inclusiveAdd;
	}

	private IEnumerable<(T, T)> EveryPair<T>(List<T> items)
	{
		for(int a = 0; a < items.Count; a++)
		{
			for(int b = a + 1; b < items.Count; b++)
			{
				yield return (items[a], items[b]);
			}
		}
	}

	private static int[] SortedInstances(List<Coordinate> stars, Func<Coordinate, int> prop)
	{
		return stars
			.Select(prop)
			.Distinct()
			.Order()
			.ToArray();
	}
}