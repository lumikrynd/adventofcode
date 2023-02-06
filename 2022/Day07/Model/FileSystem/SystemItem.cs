namespace Day07.Model.FileSystem;

public interface ISystemItem
{
	public string Name { get; }
	public int GetSize();
}
