using Helpers;
using NUnit.Framework;
using Y2023.Day14.Models;

namespace Y2023.Day14;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(136));
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
		Assert.That(result, Is.EqualTo(64));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);
		var map = new Map(model);

		var afterTilt = TiltNorth(map);

		var load = afterTilt.RoundRocks
			.Select(x => map.Height - x.Y)
			.Sum();

		Console.WriteLine($"Result: {load}");
		return load;
	}

	private long Part2(IEnumerable<string> input)
	{
		var model = Parser.Parse(input);
		var map = new Map(model);

		Dictionary<Map, long> oldStates = new();

		long cycles = 1000000000;
		bool skipped = false;
		for(long i = 0; i < cycles; i++)
		{
			map = TiltNorth(map);
			map = TiltWest(map);
			map = TiltSouth(map);
			map = TiltEast(map);

			if(oldStates.TryGetValue(map, out var firstAppearance) && !skipped)
			{
				var remaining = cycles - (i + 1);
				var cycle = i - firstAppearance;
				var addTo = remaining - (remaining % cycle);
				i += addTo;

				skipped = true;
			}
			else
			{
				oldStates[map] = i;
			}
		}

		var load = map.RoundRocks
			.Select(x => map.Height - x.Y)
			.Sum();

		Console.WriteLine($"Result: {load}");
		return load;
	}

	private Map TiltNorth(Map map)
	{
		var newStones = new List<Coordinate>();
		var stones = map.RoundRocks.ToHashSet();

		for(int x = 0; x < map.Width; x++)
		{
			var curY = 0;
			for(int y = 0; y < map.Height; y++)
			{
				var pos = new Coordinate(x, y);
				if(stones.Contains(pos))
					newStones.Add(new(x, curY++));
				if(map.SquareRocks.Contains(pos))
					curY = y + 1;
			}
		}

		return new(map, newStones);
	}
	
	private Map TiltSouth(Map map)
	{
		var newStones = new List<Coordinate>();
		var stones = map.RoundRocks.ToHashSet();

		for(int x = 0; x < map.Width; x++)
		{
			var curY = map.Height - 1;
			for(int y = curY; y >= 0; y--)
			{
				var pos = new Coordinate(x, y);
				if(stones.Contains(pos))
					newStones.Add(new(x, curY--));
				if(map.SquareRocks.Contains(pos))
					curY = y - 1;
			}
		}

		return new(map, newStones);
	}

	private Map TiltWest(Map map)
	{
		var newStones = new List<Coordinate>();
		var stones = map.RoundRocks.ToHashSet();

		for(int y = 0; y < map.Height; y++)
		{
			var curX = 0;
			for(int x = 0; x < map.Width; x++)
			{
				var pos = new Coordinate(x, y);
				if(stones.Contains(pos))
					newStones.Add(new(curX++, y));
				if(map.SquareRocks.Contains(pos))
					curX = x + 1;
			}
		}

		return new(map, newStones);
	}
	
	private Map TiltEast(Map map)
	{
		var newStones = new List<Coordinate>();
		var stones = map.RoundRocks.ToHashSet();

		for(int y = 0; y < map.Height; y++)
		{
			var curX = map.Width - 1;
			for(int x = curX; x >= 0; x--)
			{
				var pos = new Coordinate(x, y);
				if(stones.Contains(pos))
					newStones.Add(new(curX--, y));
				if(map.SquareRocks.Contains(pos))
					curX = x - 1;
			}
		}

		return new(map, newStones);
	}
}