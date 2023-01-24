using System.Collections.Generic;
using System.Linq;

namespace Day1;

internal class Parser
{
	public static List<List<int>> ParseInput(string input)
	{
		var parser = new Parser(input);
		parser.ProvisionElfs();
		return parser.Elves;
	}

	private readonly IEnumerator<string> Input;
	private readonly List<List<int>> Elves = new();

	private Parser(string input)
	{
		Input = input.Split("\n").Select(s => s.Trim()).GetEnumerator();
		Input.MoveNext();
		Elves = new();
	}

	private void ProvisionElfs()
	{
		do
		{
			ProvisionElf();
		}
		while(NextElf());
	}

	private bool NextElf() => Input.MoveNext();

	private void ProvisionElf()
	{
		var elf = new List<int>();
		GiveRations(elf);
		Elves.Add(elf);
	}

	private void GiveRations(List<int> elf)
	{
		do
		{
			var ration = CreateRation();
			elf.Add(ration);
		}
		while(NextRation());
	}

	private bool NextRation() => Input.MoveNext() && !string.IsNullOrWhiteSpace(Input.Current);

	private int CreateRation()
	{
		return int.Parse(Input.Current);
	}
}
