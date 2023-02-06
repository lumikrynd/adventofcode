using System.Collections.Generic;
using System.Linq;

namespace Day07.Model.FileSystem;

public class Directory : ISystemItem
{
	public string Name { get; private set; }
	private readonly Dictionary<string, ISystemItem> content = new();

	public Directory(string name)
	{
		Name = name;
	}

	public void AddContent(ISystemItem item)
	{
		content.Add(item.Name, item);
	}

	public int GetSize()
	{
		return content.Values.Sum(x => x.GetSize());
	}

	public ISystemItem GetChild(string name)
	{
		return content[name];
	}
}
