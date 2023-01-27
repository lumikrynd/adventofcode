using Day20.Circular;

namespace Day20;

internal partial class Program
{
	static void Main(string[] args)
	{
		UniqueTest(Example);
		UniqueTest(Input);

		Console.WriteLine();
		Console.WriteLine();

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

	private static void UniqueTest(string input)
	{
		var initial = Parser.ParseInput(input);
		var unique = initial.Distinct().ToList();
		Console.WriteLine($"All are unique: {initial.Count == unique.Count}");
	}

	private static void Part1(string input)
	{
		var numbers = Parser.ParseInput(input);

		var circular = new CircularList<int>(numbers);
		var sequence = circular.ToArray();

		LinkedItem<int>? zero = null;

		foreach(var instruction in sequence)
		{
			var count = instruction.Value;

			for(int i = 0; i < count; i++)
				circular.MoveForward(instruction);

			for(int i = 0; i > count; i--)
				circular.MoveBackward(instruction);

			if(count == 0)
				zero = instruction;
		}

		if(zero == null)
			throw new Exception();

		var current = zero;
		var score = 0;
		for(int i = 0; i < 1000; i++)
		{
			current = current.Next;
		}
		Console.WriteLine($"Value 1 : {current.Value}");
		score += current.Value;

		for(int i = 0; i < 1000; i++)
		{
			current = current.Next;
		}
		Console.WriteLine($"Value 2 : {current.Value}");
		score += current.Value;

		for(int i = 0; i < 1000; i++)
		{
			current = current.Next;
		}
		Console.WriteLine($"Value 3 : {current.Value}");
		score += current.Value;

		Console.WriteLine($"Sum : {score}");
	}

	private static void Part2(string input)
	{
		var numbers = Parser.ParseInput(input).Select(x => (long)x * (long)811589153);

		var circular = new CircularList<long>(numbers);
		var sequence = circular.ToArray();

		if(sequence.Length != numbers.Count())
			throw new Exception();

		LinkedItem<long>? zero = null;

		var modularVal = sequence.Length - 1;

		for(int x = 0; x < 10; x++)
		{
			foreach(var instruction in sequence)
			{
				var count = instruction.Value;
				count %= modularVal;

				for(int i = 0; i < count; i++)
					circular.MoveForward(instruction);

				for(int i = 0; i > count; i--)
					circular.MoveBackward(instruction);

				if(count == 0)
					zero = instruction;
			}
		}

		if(zero == null)
			throw new Exception();

		long score = CalculateScore(zero);

		Console.WriteLine($"Sum : {score}");
	}

	private static long CalculateScore(LinkedItem<long>? zero)
	{
		if(zero == null) throw new Exception();

		var current = zero;
		long score = 0;
		for(int i = 0; i < 3000; i++)
		{
			current = current.Next;

			if((i + 1) % 1000 == 0)
			{
				score += current.Value;
				Console.WriteLine($"Value {(i + 1) / 1000} : {current.Value}");
			}
		}

		return score;
	}
}
