namespace Day15.Model;

internal interface INode
{
	int SideLengthPowerOfTwo { get; }
	bool HasContent(int x, int y);
	public INode Combine(INode other);
}
