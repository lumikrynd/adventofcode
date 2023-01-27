namespace Day17;

internal partial class Program
{
	static void Main(string[] args)
	{
		Part1(Example);

		Console.WriteLine();
		Console.WriteLine();

		Part1(Input);

		Console.WriteLine();
		Console.WriteLine();

		Part2(Example);

		Console.ReadKey();

		Console.WriteLine();
		Console.WriteLine();

		Part2(Input);
	}

	static void Part1(string input)
	{
		var wind = new Wind(input);
		var rockShapeGenerator = new RockPatternGenerator();
		var map = new Map();

		AddRocksToMap(map, 2022, wind, rockShapeGenerator);

		Console.WriteLine($"Tower height: {map.Height()}");
	}

	static void Part2(string input)
	{
		var wind = new Wind(input);
		var rockShapeGenerator = new RockPatternGenerator();
		var map = new Map();

		int length = input.Length * 5;
		int offset = (int)(1000000000000 % length);

		AddRocksToMap(map, offset, wind, rockShapeGenerator);
		int previous = map.Height();


		Console.WriteLine(length);
		Console.WriteLine($"Tower height: {previous}");

		Dictionary<int, List<int>> diffs = new();

		for(int i = 0; i < 1000; i++)
		{
			AddRocksToMap(map, length, wind, rockShapeGenerator);
			var current = map.Height();
			Console.WriteLine($"{i} : {current} , diff: {current - previous}");
			AddToListDict(diffs, (current - previous), i);
			previous = current;
		}
	}

	private static void AddToListDict(Dictionary<int, List<int>> dict, int count, int index)
	{
		List<int>? list;
		if(!dict.TryGetValue(count, out list))
		{
			list = new();
			dict[count] = list;
		}
		list.Add(index);
	}

	private static void AddRocksToMap(Map map, int count, Wind wind, RockPatternGenerator rockShapeGenerator)
	{
		for (int i = 0; i < count; i++)
		{
			var shape = rockShapeGenerator.GetNext();
			var rock = new Rock(shape, map.Height() + 3);
			rock.MoveRight();
			rock.MoveRight();

			var rockIsActive = true;
			while (rockIsActive)
			{
				RockWindMotion(wind, map, rock);

				rock.MoveDown();
				if (rock.Altitude < 0 || map.CheckForCollision(rock))
				{
					rock.MoveUp();
					map.PlaceRock(rock);
					rockIsActive = false;
				}
			}
		}
	}

	private static void RockWindMotion(Wind wind, Map map, Rock rock)
	{
		var dir = wind.GetWindDir();
		if (dir == -1)
		{
			rock.MoveLeft();
			if (map.CheckForCollision(rock))
			{
				rock.MoveRight();
			}
		}
		else
		{
			rock.MoveRight();
			if (map.CheckForCollision(rock))
			{
				rock.MoveLeft();
			}
		}
	}
}