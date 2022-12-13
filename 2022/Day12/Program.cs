using System;
using Day12.Maps;
using Day12.Pathfinding;

namespace Day12;
internal partial class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");

		var map = MapParser.ParseMap(TextMap);

		var path = BreathFirstSearch.CalculatePath(map);

		Console.WriteLine(path.Count);
	}
}