using Helpers;
using NUnit.Framework;
using Y2023.Day16.Models;

namespace Y2023.Day16;

public class Challenge
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	static IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		Assert.That(result, Is.EqualTo(46));
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
		Assert.That(result, Is.EqualTo(51));
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	record Vector(Coordinate coord, Direction direction);

	private long Part1(IEnumerable<string> input)
	{
		var map = Parser.Parse(input);
		var mirrors = map.Mirrors
			.ToDictionary(m => m.Location);

		int result = CountHitFields(map, mirrors, new(new(-1, 0), Direction.East));
		Console.WriteLine($"Result: {result}");
		return result;
	}

	private long Part2(IEnumerable<string> input)
	{
		var map = Parser.Parse(input);
		var mirrors = map.Mirrors
			.ToDictionary(m => m.Location);

		int max = 0;
		foreach(var beamConfig in BeamConfigurations(map))
		{
			var count = CountHitFields(map, mirrors, beamConfig);
			max = Math.Max(max, count);
		}

		Console.WriteLine($"Result: {max}");
		return max;
	}

	private IEnumerable<Vector> BeamConfigurations(Map map)
	{
		for(int x = 0; x <= map.Width; x++)
		{
			yield return new(new(x, -1), Direction.South);
			yield return new(new(x, map.Height), Direction.North);
		}
		for(int y = 0; y <= map.Height; y++)
		{
			yield return new(new(-1, y), Direction.East);
			yield return new(new(map.Width, y), Direction.West);
		}
	}

	private int CountHitFields(Map map, Dictionary<Coordinate, Mirror> mirrors, Vector start)
	{
		Queue<Vector> beams = new([start]);

		HashSet<Vector> visitedMirrors = new();
		HashSet<Coordinate> visitedFields = new();

		while(beams.Any())
		{
			var beam = beams.Dequeue();
			var coordinate = beam.coord;
			var direction = beam.direction;

			do
			{
				coordinate = Travel(coordinate, direction);
				if(!WithinMap(coordinate, map))
					goto SkipAddition;
				visitedFields.Add(coordinate);
			}
			while(!mirrors.ContainsKey(coordinate));

			if(!visitedMirrors.Add(new(coordinate, direction)))
				continue;

			var newBeams = CreateBeams(mirrors[coordinate], direction);
			foreach(var @new in newBeams)
			{
				beams.Enqueue(@new);
			}

		SkipAddition:;
		}

		var result = visitedFields.Count;
		return result;
	}

	private bool WithinMap(Coordinate coord, Map map)
	{
		return coord.X >= 0 && coord.X < map.Width
			&& coord.Y >= 0 && coord.Y < map.Height;
	}

	private List<Vector> CreateBeams(Mirror mirror, Direction direction)
	{
		var coord = mirror.Location;
		var mirrorType = mirror.Type;
		if(mirrorType is MirrorType.Main or MirrorType.Anti)
		{
			var newDirection = (Direction)((int)Inverted(direction) ^ (int)mirrorType);
			return [new(coord, newDirection)];
		}
		if(ShouldSplit(direction, mirrorType))
		{
			var dir1 = (Direction)((int)direction ^ (int)MirrorType.Main);
			var dir2 = (Direction)((int)direction ^ (int)MirrorType.Anti);
			return [new(coord, dir1), new(coord, dir2)];
		}

		return [new(coord, direction)];
	}

	private static Direction Inverted(Direction dir) => dir switch
	{
		Direction.North => Direction.South,
		Direction.East => Direction.West,
		Direction.West => Direction.East,
		Direction.South => Direction.North,
		_ => throw new NotImplementedException(),
	};

	private static bool ShouldSplit(Direction direction, MirrorType type)
	{
		if(direction is Direction.East or Direction.West)
			return type is MirrorType.Vertical;

		return type is MirrorType.Horizontal;
	}

	private Coordinate Travel(Coordinate coordinate, Direction direction) => direction switch
	{
		Direction.North => coordinate.AddVector((0, -1)),
		Direction.East => coordinate.AddVector((1, 0)),
		Direction.West => coordinate.AddVector((-1, 0)),
		Direction.South => coordinate.AddVector((0, 1)),
		_ => throw new NotImplementedException(),
	};
}