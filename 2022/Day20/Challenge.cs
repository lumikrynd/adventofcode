using Y2022.Day20.Circular;
using Helpers;

namespace Y2022.Day20;

public class Challenge(string input) : ISolver
{
	public Challenge(IEnumerable<string> input) : this(string.Join('\n', input))
	{
	}

	public string Part1()
	{
		var numbers = Parser.ParseInput(input);

		var circular = new Circular.CircularList<int>(numbers);
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
		score += current.Value;

		for(int i = 0; i < 1000; i++)
		{
			current = current.Next;
		}
		score += current.Value;

		for(int i = 0; i < 1000; i++)
		{
			current = current.Next;
		}
		score += current.Value;

		return $"{score}";
	}

	public string Part2()
	{
		var numbers = Parser.ParseInput(input).Select(x => (long)x * (long)811589153);

		var circular = new Circular.CircularList<long>(numbers);
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

		return $"{score}";
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
			}
		}

		return score;
	}
}