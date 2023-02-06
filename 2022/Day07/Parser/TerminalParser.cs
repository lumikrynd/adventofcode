using System;
using System.Collections.Generic;
using Day07.Model.Commands;
using Day07.Model.FileSystem;
using Helpers;

namespace Day07.Parser;

public class TerminalParser
{
	public static List<ICommand> Parse(IEnumerable<string> input)
	{
		var parser = new TerminalParser(input);
		return parser.Parse();
	}

	EnumeratorWrapper<string> Input { get; }
	List<ICommand> Commands { get; } = new();

	public TerminalParser(IEnumerable<string> input)
	{
		Input = new(input);
	}

	public List<ICommand> Parse()
	{
		ICommand first = ParseCommand();
		Commands.Add(first);
		while(Input.HasNext())
		{
			Input.MoveNext();
			ICommand command = ParseCommand();
			Commands.Add(command);
		}
		return Commands;
	}

	private ICommand ParseCommand()
	{
		if(CurrentStartWith("$ cd"))
			return ParseCD();
		if(CurrentStartWith("$ ls"))
			return ParseLS();

		throw new Exception("... what is this !#¤%");
	}

	private bool CurrentStartWith(string start) => Input.Current.StartsWith(start);

	private ICommand ParseCD()
	{
		var parts = CurrentSplit();
		return new CD(parts[2]); //$ cd dir
	}

	private ICommand ParseLS()
	{
		List<ISystemItem> items = new();
		while(NextDoesntStartWith("$"))
		{
			Input.MoveNext();
			var systemItem = ParseSystemItem();
			items.Add(systemItem);
		}
		return new LS(items);
	}

	private bool NextDoesntStartWith(string start) => Input.HasNext() && !Input.Peek().StartsWith(start);

	private ISystemItem ParseSystemItem()
	{
		if(CurrentStartWith("dir"))
			return ParseDir();
		else
			return ParseFile();
	}

	private Directory ParseDir()
	{
		var parts = CurrentSplit();
		var name = parts[1];
		return new Directory(name);
	}

	private File ParseFile()
	{
		var parts = CurrentSplit();
		var size = int.Parse(parts[0]);
		var name = parts[1];
		return new File(name, size);
	}

	private string[] CurrentSplit() => Input.Current.Split(' ');
}
