namespace Day15.Model;

internal class Wrapper
{
	public INode RootNode { get; private set; }

	public (int x, int y) LowerBound { get; }

	public Wrapper(INode rootNode, (int, int) lowerBound)
	{
		RootNode = rootNode;
		LowerBound = lowerBound;
	}
}
