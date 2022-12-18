using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day1;

internal class Parser : IDisposable
{
	public static List<List<int>> ParseInput(string input)
	{
		using var parser = new Parser(input);

		return parser.ParseInput();
	}

	readonly StringReader Stream;

	private Parser(string input)
	{
		Stream = new StringReader(input);
	}

	public void Dispose()
	{
		Stream.Dispose();
	}

	private List<List<int>> ParseInput()
	{
		var elves = new List<List<int>>();

		while(Stream.Peek() != -1)
		{
			var elf = new List<int>();

			while(true)
			{
				var line = Stream.ReadLine();
				if(string.IsNullOrEmpty(line))
					break;

				elf.Add(int.Parse(line));
			}

			elves.Add(elf);
		}

		return elves;
	}
}
