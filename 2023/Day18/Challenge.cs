using Helpers;
using Y2023.Day18.Models;

namespace Y2023.Day18;

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