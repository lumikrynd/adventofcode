using Helpers;

namespace Y2022.Day06;

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