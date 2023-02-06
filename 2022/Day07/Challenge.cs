using System;
using System.Collections.Generic;
using System.Linq;
using Day07.Model.Commands;
using Day07.Model.FileSystem;
using Day07.Parser;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;

using Directory = Day07.Model.FileSystem.Directory;
using File = System.IO.File;

namespace Day05;

public class Challenge
{
	IEnumerable<string> ExampleInput => File.ReadLines(@"Input/Example.txt");
	IEnumerable<string> PuzzleInput => File.ReadLines(@"Input/Puzzle.txt");

	[Test]
	public void Part1_Example()
	{
		var result = Part1(ExampleInput);
		result.Should().Be(95437);
	}

	[Test]
	public void Part1_MainPuzzle()
	{
		Part1(PuzzleInput);
	}

	[Test]
	public void Part2_Example()
	{
		var result = Part2(ExampleInput);
		result.Should().Be(24933642);
	}

	[Test]
	public void Part2_MainPuzzle()
	{
		Part2(PuzzleInput);
	}

	public long Part1(IEnumerable<string> data)
	{
		IEnumerable<Directory> directories = DirectoriesList(data);

		long sum = 0;
		foreach(var dir in directories)
		{
			if(dir.GetSize() <= 100000)
				sum += dir.GetSize();
		}

		Console.Write($"sum: {sum}");
		return sum;
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

	public int Part2(IEnumerable<string> data)
	{
		const int CAPACITY = 70000000;
		const int NEEDED = 30000000;

		IEnumerable<Directory> directories = DirectoriesList(data);
		var root = directories.First();

		var unusedSpace = CAPACITY - root.GetSize();
		var needToFree = NEEDED - unusedSpace;

		Console.WriteLine($"Unused: {unusedSpace:N1}");
		Console.WriteLine($"Need to free: {needToFree:N1}");

		var toRemove = directories
			.Where(x => x.GetSize() >= needToFree)
			.MinBy(x => x.GetSize());

		Console.WriteLine($"Remove dir {toRemove!.Name} with size {toRemove!.GetSize():N1}");

		return toRemove!.GetSize();
	}
}
