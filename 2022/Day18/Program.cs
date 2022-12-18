using System.Text.RegularExpressions;

namespace Day18;

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

		Console.WriteLine();
		Console.WriteLine();

		Part2(Input);
	}

	static void Part1(string input)
	{
		var coordinates = Parser.ParseInput(input);

		var boulders = new HashSet<(int, int, int)>();

		var surface = 0;

		foreach(var coord in coordinates)
		{
			boulders.Add(coord);
			surface += 6;

			surface -= 2 * CountExistingNeighbors(boulders, coord);
		}

		Console.WriteLine($"NumberOfRocks: {coordinates.Count}");
		Console.WriteLine($"Surface: {surface}");
	}

	static void Part2(string input)
	{
		var coordinates = Parser.ParseInput(input);

		var boulders = coordinates.ToHashSet();

		var upper = (
			coordinates.Max(c => c.x) + 1,
			coordinates.Max(c => c.y) + 1,
			coordinates.Max(c => c.z) + 1
			);

		var lower = (
			coordinates.Min(c => c.x) - 1,
			coordinates.Min(c => c.y) - 1,
			coordinates.Min(c => c.z) - 1
			);

		var toVisit = new Queue<(int x, int y, int z)>();
		toVisit.Enqueue(lower);

		var seen = new HashSet<(int x, int y, int z)>();
		seen.Add(lower);

		var surface = 0;

		while (toVisit.Any())
		{
			var current = toVisit.Dequeue();

			var neighbors = GenerateNeigbors(current);

			foreach(var nb in neighbors)
			{
				if (!InsideBound(nb, lower, upper))
					continue;

				if (boulders.Contains(nb))
				{
					surface++;
					continue;
				}

				if (seen.Contains(nb))
					continue;

				seen.Add(nb);
				toVisit.Enqueue(nb);
			}
		}

		Console.WriteLine($"NumberOfRocks: {coordinates.Count}");
		Console.WriteLine($"Surface: {surface}");
	}

	static bool InsideBound((int x, int y, int z) point, (int x, int y, int z) lower, (int x, int y, int z) upper)
	{
		return
			point.x >= lower.x && point.x <= upper.x &&
			point.y >= lower.y && point.y <= upper.y &&
			point.z >= lower.z && point.z <= upper.z;
	}

	static int CountExistingNeighbors(HashSet<(int, int, int)> existing, (int x, int y, int z) point)
	{
		var neighbors = GenerateNeigbors(point);

		int count = 0;
		foreach (var n in neighbors)
		{
			if (existing.Contains(n))
				count++;
		}

		return count;
	}

	private static List<(int x, int y, int z)> GenerateNeigbors((int x, int y, int z) point)
	{
		return new List<(int x, int y, int z)>()
		{
			(point.x + 1, point.y, point.z),
			(point.x - 1, point.y, point.z),
			(point.x, point.y + 1, point.z),
			(point.x, point.y - 1, point.z),
			(point.x, point.y, point.z + 1),
			(point.x, point.y, point.z - 1),
		};
	}
}
