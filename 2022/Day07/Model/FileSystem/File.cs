namespace Day07.Model.FileSystem;

public class File : ISystemItem
{
	public string Name { get; private set; }
	public int Size { get; private set; }

	public File(string name, int size)
	{
		Name = name;
		Size = size;
	}

	int ISystemItem.GetSize()
	{
		return Size;
	}
}
