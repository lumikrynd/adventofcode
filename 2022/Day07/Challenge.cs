using Y2022.Day07.Model.Commands;
using Y2022.Day07.Model.FileSystem;
using Y2022.Day07.Parser;
using FluentAssertions;
using Helpers;
using NUnit.Framework;

using Directory = Y2022.Day07.Model.FileSystem.Directory;
using File = System.IO.File;

namespace Y2022.Day07;

public class Test
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");

	[Test]
	public void Part1_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part1();
		result.Should().Be(95437.ToString());
	}

	[Test]
	public void Part2_Example()
	{
		var challenge = new Challenge(ExampleInput);
		var result = challenge.Part2();
		result.Should().Be(24933642.ToString());
	}
}

public class Challenge(params IEnumerable<string> data) : ISolver
{
	public string Part1()
	{
		IEnumerable<Directory> directories = DirectoriesList(data);

		long sum = 0;
		foreach(var dir in directories)
		{
			if(dir.GetSize() <= 100000)
				sum += dir.GetSize();
		}

		return sum.ToString();
	}

	private static IEnumerable<Directory> DirectoriesList(IEnumerable<string> data)
	{
		var terminalCommands = TerminalParser.Parse(data);

		FileSystem fileSystem = new FileSystem();
		fileSystem.RecreateCommandResults(terminalCommands);

		var directories = terminalCommands
			.OfType<LS>()
			.SelectMany(ls => ls.Content)
			.OfType<Directory>();

		return directories.Prepend(fileSystem.GetRoot());
	}

	public string Part2()
	{
		const int CAPACITY = 70000000;
		const int NEEDED = 30000000;

		IEnumerable<Directory> directories = DirectoriesList(data);
		var root = directories.First();

		var unusedSpace = CAPACITY - root.GetSize();
		var needToFree = NEEDED - unusedSpace;

		var toRemove = directories
			.Where(x => x.GetSize() >= needToFree)
			.MinBy(x => x.GetSize());

		return toRemove!.GetSize().ToString();
	}
}