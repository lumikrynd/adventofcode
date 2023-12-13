using Helpers;
using NUnit.Framework;
using Y2023.Day10.Models;

namespace Y2023.Day10;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> ExampleInput2 => File.ReadLines(@"Input/Example2.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(8));
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput2);
		Assert.That(result, Is.EqualTo(10));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private long Part1(IEnumerable<string> input)
	{
		var map = Parser.Parse(input);

		int length = 1;
		var start = map.Start;
		var previous = start;
		var current = Connecting(start, map)
			.First(c => Connecting(c, map).Contains(start));

		while(current != start)
		{
			var next = Connecting(current, map).Single(x => x != previous);
			previous = current;
			current = next;
			length++;
		}

		var result = length / 2;
		Console.Write($"Result: {result}");
		return result;
	}

	private long Part2(IEnumerable<string> input)
	{
		var map = Parser.Parse(input);

		var loopPipes = FindLoopPipes(map);
		var connectedRight = FindPipesConnectedRight(map, loopPipes);

		int enclosed = 0;

		var xLength = map.Pipes.GetLength(0);
		var yLength = map.Pipes.GetLength(1);

		for(int x = 0; x < xLength; x++)
		{
			for(int y = 0; y < yLength; y++)
			{
				if(IsEnclosed(loopPipes, connectedRight, x, y))
					enclosed++;
			}
		}

		Console.Write($"Result: {enclosed}");
		return enclosed;
	}

	private bool IsEnclosed(HashSet<Coordinate> loop, HashSet<Coordinate> connectedRight, int x, int y)
	{
		if(loop.Contains(new(x, y)))
			return false;

		var counter = 0;
		do
		{
			y--;
			if(connectedRight.Contains(new(x, y)))
				counter++;
		}
		while(y > 0);

		return counter % 2 == 1;
	}

	private HashSet<Coordinate> FindLoopPipes(Map map)
	{
		HashSet<Coordinate> loopPipes = new();
		var start = map.Start;
		var previous = start;
		var current = Connecting(start, map)
			.First(c => Connecting(c, map).Contains(start));

		while(current != start)
		{
			loopPipes.Add(current);

			var next = Connecting(current, map).Single(x => x != previous);
			previous = current;
			current = next;
		}

		return loopPipes;
	}

	private HashSet<Coordinate> FindPipesConnectedRight(Map map, HashSet<Coordinate> LoopPipes)
	{
		var start = map.Start;
		var ConnectedRight = LoopPipes
			.Where(c => map.Pipes[c.X, c.Y].HasFlag(PipeType.Right) && c != start)
			.ToHashSet();

		var startConnected = Connecting(start, map)
			.Where(c => Connecting(c, map).Contains(start)).ToList();

		var rightOfStart = start.AddVector((1, 0));
		if(startConnected.Contains(rightOfStart))
			ConnectedRight.Add(start);
		return ConnectedRight;
	}

	private List<Coordinate> Connecting(Coordinate coordinate, Map map)
	{
		List<Coordinate> result = new();
		var type = map.Pipes[coordinate.X, coordinate.Y];

		if(type.HasFlag(PipeType.Up))
			result.Add(coordinate.AddVector((0, -1)));

		if(type.HasFlag(PipeType.Down))
			result.Add(coordinate.AddVector((0, 1)));

		if(type.HasFlag(PipeType.Left))
			result.Add(coordinate.AddVector((-1, 0)));

		if(type.HasFlag(PipeType.Right))
			result.Add(coordinate.AddVector((1, 0)));

		return result.Where(c => IsWithinMap(c, map)).ToList();
	}

	private bool IsWithinMap(Coordinate coordinate, Map map)
	{
		var xLength = map.Pipes.GetLength(0);
		var yLength = map.Pipes.GetLength(1);

		return
			coordinate.X >= 0 &&
			coordinate.Y >= 0 &&
			coordinate.X < xLength &&
			coordinate.Y < yLength;
	}
}