using Helpers;

namespace Day15.Model;

internal interface IMap
{
	bool HasContent(Coordinate coordinate);
	int CountRowCowerage(int row);
}
