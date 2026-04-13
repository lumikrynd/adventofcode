using System.Diagnostics.CodeAnalysis;
using Helpers;

namespace Y2022.Day15.Model;

internal interface IMap
{
	bool HasContent(Coordinate coordinate);
	int CountRowCowerage(int row);
	bool TryGetUncoveredSpot(Coordinate min, Coordinate max, [NotNullWhen(true)] out Coordinate? spot);
}