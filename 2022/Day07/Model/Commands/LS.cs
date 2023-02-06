using System.Collections.Generic;
using System.IO;
using Day07.Model.FileSystem;

namespace Day07.Model.Commands;

public class LS : ICommand
{
	public List<ISystemItem> Content { get; }

	public LS(List<ISystemItem> content)
	{
		Content = content;
	}

	public override string ToString()
	{
		StringWriter sw = new StringWriter();
		sw.WriteLine("ls");
		foreach(var item in Content)
		{
			sw.WriteLine($"    {item.Name}");
		}
		return sw.ToString();
	}
}
