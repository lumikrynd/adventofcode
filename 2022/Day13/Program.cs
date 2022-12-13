namespace Day13;

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

	private static void Part1(string input)
	{
		var data = Parser.ParseInput(input);

		var sum = 0;
		for (int i = 0; i < data.Count; i++)
		{
			var current = data[i];
			var comparison = current.Left.CompareTo(current.Right);

			if (comparison <= 0)
				sum += i + 1;

			Console.WriteLine($"{i} : {comparison <= 0}");
		}

		Console.WriteLine(sum);
	}

	private static void Part2(string input)
	{
		var data = Parser.ParseInput(input);
		var extra = Parser.ParseInput(Extra).First();

		int leftCount = 0, rightCount = 0;

		if (extra.Left.CompareTo(extra.Right) >= 0)
			throw new Exception();

		foreach (var d in data)
		{
			if (extra.Left.CompareTo(d.Left) > 0) leftCount++;
			if (extra.Left.CompareTo(d.Right) > 0) leftCount++;

			if (extra.Right.CompareTo(d.Left) > 0) rightCount++;
			if (extra.Right.CompareTo(d.Right) > 0) rightCount++;
		}

		Console.WriteLine($"Left count : {leftCount}");
		Console.WriteLine($"Right count : {rightCount}");

		var result = (leftCount + 1) * (rightCount + 2);
		Console.WriteLine($"Result : {result}");
	}
}