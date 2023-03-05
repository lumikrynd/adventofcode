using Helpers;

namespace Day14.Model;

internal class CaveMapWithFloor : CaveMap
{
	public CaveMapWithFloor(List<RockFormation> rocks) : base(rocks) { }

	public override int Bottom => base.Bottom + 2;

	public override bool IsFilledOutAt(Coordinate coordinate)
	{
		if(coordinate.Y >= Bottom)
			return true;
		return base.IsFilledOutAt(coordinate);
	}
}
