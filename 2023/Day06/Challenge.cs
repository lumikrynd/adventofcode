using NUnit.Framework;

namespace Y2023.Day06;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(288));
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		Assert.That(result, Is.EqualTo(71503));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var races = Parser.Parse(input);

		long total = 1;
		foreach(var race in races)
		{
			var score = CountWaysToWin(race);
			total *= score;
		}

		Console.Write($"Result: {total}");
		return total;
	}

	private long Part2(IEnumerable<string> input)
	{
		var race = Parser.ParseSingle(input);

		var score = CountWaysToWin(race);

		Console.Write($"Result: {score}");
		return score;
	}

	private long CountWaysToWin((long time, long distance) race)
	{
		bool Wins(long wait) => CalculateDistance(wait, race.time) > race.distance;

		var (minApprox, maxApprox) = CalculateRoots(race.time, race.distance);

		var min = (long)Math.Ceiling(minApprox);
		var max = (long)Math.Floor(maxApprox);

		if(Wins(min))
		{
			while(Wins(min - 1))
				min--;
		}
		else
		{
			while(!Wins(min))
				min++;
		}
		if(Wins(max))
		{
			while(Wins(max + 1))
				max++;
		}
		else
		{
			while(!Wins(max))
				max--;
		}

		return max - min + 1;
	}

	long CalculateDistance(long wait, long time) => wait * (time - wait);

	/// <summary>
	/// wait * (time - wait) => -1 * wait^2 + time * wait + 0
	/// Using formula for solving  -1 wait^2 + time * wait - minimum
	/// </summary>
	(double, double) CalculateRoots(long time, long minimum)
	{
		var a = -1;
		var b = time;
		var c = -minimum;
		var d = Math.Pow(b, 2) - 4 * a * c;
		var dr = Math.Sqrt(d);

		var min = (-b + dr) / (2 * a);
		var max = (-b - dr) / (2 * a);
		return (min, max);
	}

}