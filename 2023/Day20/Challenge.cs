using Helpers;

namespace Y2023.Day20;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var modules = Parser.Parse(input);
		var emulator = new Emulator(modules);

		emulator.PushButton(1000);

		var counts = emulator.GetCounts();
		var result = 1L * counts.Low * counts.High;

		return result.ToString();
	}

	public string Part2()
	{
		var modules = Parser.Parse(input);
		var emulator = new Emulator(modules);

		return "";
	}
}