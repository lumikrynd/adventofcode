namespace Day15.Model;

internal interface INode :IMap
{
	public INode Combine(INode other);
}
