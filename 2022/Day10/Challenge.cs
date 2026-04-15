using System.Text;
using Y2022.Day10.Parsing;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

namespace Y2022.Day10;

public class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Day10/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be("13140");
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be(
@"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
");
	}
}

public class Challenge(IEnumerable<string> puzzleInput) : ISolver
{
	public string Part1()
	{
		var instructions = InstructionsParser.Parse(puzzleInput);
		var runner = new CommandRunner(instructions);

		int sum = 0;
		for(int i = 20; i <= 220; i += 40)
		{
			sum += i * runner.GetXValue(i);
		}

		return sum.ToString();
	}

	public string Part2()
	{
		const int WIDTH = 40;
		const int HEIGHT = 6;

		var instructions = InstructionsParser.Parse(puzzleInput);
		var runner = new CommandRunner(instructions);
		var screen = new StringBuilder();

		int cycle = 1;
		for(; cycle <= WIDTH * HEIGHT; cycle++)
		{
			var dist = distance(XPos(), runner.GetXValue(cycle));
			char symbol = dist <= 1 ? '#' : '.';
			screen.Append(symbol);

			if(cycle % WIDTH == 0)
				screen.AppendLine();
		}

		return screen.ToString();

		int XPos() => ((cycle - 1) % WIDTH);
		int distance(int a, int b) => Math.Abs(a - b);
	}
}