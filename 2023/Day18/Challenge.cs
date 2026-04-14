using Helpers;
using NUnit.Framework;
using Y2023.Day18.Models;

namespace Y2023.Day18;

public class Test
{
	static IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		Assert.That(result, Is.EqualTo("62"));
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		Assert.That(result, Is.EqualTo("952408144115"));
	}
}

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var digPlan = Parser.Parse(input);
		var relDigPlan = MakeRelative(digPlan);

		long lavaArea = Digger.CalculateLavaArea(relDigPlan); ;
		return lavaArea.ToString();
	}

	public string Part2()
	{
		var digPlan = Parser.AltParse(input);
		var relDigPlan = MakeRelative(digPlan);

		long lavaArea = Digger.CalculateLavaArea(relDigPlan); ;
		return lavaArea.ToString();
	}

	private List<RelativeDigStep> MakeRelative(List<DigStep> digPlan)
	{
		return digPlan.ToCircularList()
			.Select(n => MakeRelative(n.Item, n.Next.Item))
			.ToList();
	}

	private RelativeDigStep MakeRelative(DigStep current, DigStep next)
	{
		return new(current.Length, CalculateTurn(current.Direction, next.Direction));
	}

	private Turn CalculateTurn(Direction curDir, Direction nextDir)
	{
		if(nextDir == curDir.SpinClockwise())
			return Turn.Right;
		if(nextDir == curDir.SpinCounterClockwise())
			return Turn.Left;

		throw new ArgumentException("Something is wrong");
	}
}