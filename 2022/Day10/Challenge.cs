using System.Text;
using Day10.Parsing;
using FluentAssertions;
using NUnit.Framework;

namespace Day10;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");
	IEnumerable<string> SmallInput => File.ReadLines(@"Input/SmallInput.txt");

	[Test]
	public void TestPrint_SmallInput()
	{
		var runner = TestPrint(SmallInput);
		runner.GetXValue(1).Should().Be(1);
		runner.GetXValue(2).Should().Be(1);
		runner.GetXValue(3).Should().Be(1);
		runner.GetXValue(4).Should().Be(4);
		runner.GetXValue(5).Should().Be(4);
		runner.GetXValue(6).Should().Be(0);
	}

	[Test]
	public void TestPrint_Example()
	{
		TestPrint(ExampleInput);
	}

	[Test]
	public void TestPrint_MainPuzzle()
	{
		TestPrint(PuzzleInput);
	}

	private CommandRunner TestPrint(IEnumerable<string> puzzleInput)
	{
		var instructions = InstructionsParser.Parse(puzzleInput);
		var runner = new CommandRunner(instructions);

		for(int i = 1; i <= 240; i++)
		{
			Console.WriteLine($"Value {i}: {runner.GetXValue(i)}");
		}

		return runner;
	}

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(13140);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		var result = Part1(PuzzleInput);
		result.Should().Be(12640);
	}

	private int Part1(IEnumerable<string> puzzleInput)
	{
		var instructions = InstructionsParser.Parse(puzzleInput);
		var runner = new CommandRunner(instructions);

		int sum = 0;
		for(int i = 20; i <= 220; i += 40)
		{
			sum += i * runner.GetXValue(i);
		}

		Console.WriteLine($"Sum: {sum}");
		return sum;
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(
@"##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....
");
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	private string Part2(IEnumerable<string> puzzleInput)
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

		Console.Write(screen.ToString().Replace('.', ' '));

		return screen.ToString();

		int XPos() => ((cycle - 1) % WIDTH);
		int distance(int a, int b) => Math.Abs(a - b);
	}
}