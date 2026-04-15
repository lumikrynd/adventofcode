using Y2022.Day14.Model;
using Helpers;

namespace Y2022.Day14;

public class Challenge(IEnumerable<string> textMap) : ISolver
{
	public string Part1()
	{
		var sandSource = new Coordinate(500, 0);
		var caveSpecification = CaveSpecificationParser.Parse(textMap);
		var cave = new CaveMap(caveSpecification);
		var SandSimulator = new SandSimulator(cave);
		SandSimulator.FillSandFrom(sandSource);

		return SandSimulator.SandFilled.ToString();
	}

	public string Part2()
	{
		var sandSource = new Coordinate(500, 0);
		var caveSpecification = CaveSpecificationParser.Parse(textMap);
		var cave = new CaveMapWithFloor(caveSpecification);
		var SandSimulator = new SandSimulator(cave);
		SandSimulator.FillSandFrom(sandSource);

		return SandSimulator.SandFilled.ToString();
	}
}