using Helpers;
using Y2023.Day19.Models;

namespace Y2023.Day19;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var model = Parser.Parse(input);
		var interpreter = new Interpreter(model.Workflows);

		long sum = model.Parts
			.Where(interpreter.SortPart)
			.Sum(p => p.X + p.M + p.A + p.S);

		return sum.ToString();
	}

	public string Part2()
	{
		var model = Parser.Parse(input);
		var range = new CatRange(1, 4000);
		var partRange = new PartRange(range, range, range, range);

		var calculator = new PreCalculator(model.Workflows);
		var accepted = calculator.CalculateAccepted(partRange);

		long count = 0;
		foreach(var pr in accepted)
		{
			count += pr.Combinations;
		}

		return count.ToString();
	}
}