using Y2022.Day12.Maps;
using Y2022.Day12.Pathfinding;
using Helpers;

namespace Y2022.Day12;

public class Challenge(IEnumerable<string> textMap) : ISolver
{
	public string Part1()
	{
		var map = MapParser.ParseMap(textMap.ToArray());
		map.Start = map.S;
		map.FoundEnd = coord => coord.Equals(map.E);
		map.StepPossible = NormalTraverselStepChecker(map);

		var path = BreathFirstSearch.CalculatePath(map);
		return path.Count.ToString();
	}

	private Func<Coordinate, Coordinate, bool> NormalTraverselStepChecker(Map map)
	{
		return (from, to) =>
		{
			var maxHeigh = map.GetHeightAt(from) + 1;
			return map.GetHeightAt(to) <= maxHeigh;
		};
	}

	public string Part2()
	{
		var map = MapParser.ParseMap(textMap.ToArray());
		map.Start = map.E;
		map.FoundEnd = coord => map.GetHeightAt(coord) <= 0;
		map.StepPossible = InvertedTraverselStepChecker(map);

		var path = BreathFirstSearch.CalculatePath(map);
		return path.Count.ToString();
	}

	private Func<Coordinate, Coordinate, bool> InvertedTraverselStepChecker(Map map)
	{
		var temp = NormalTraverselStepChecker(map);
		return (from, to) => temp(to, from);
	}
}