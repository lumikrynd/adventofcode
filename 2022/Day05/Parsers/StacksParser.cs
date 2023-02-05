using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Day05.Models;

namespace Day05.Parsers;

internal class StacksParser
{
	public static Stack<char>[] Parse(IEnumerable<string> input)
	{
		var parser = new StacksParser(input.Reverse());
		return parser.GetSupplyStacks();
	}

	private Stack<char>[] stacks;

	public StacksParser(IEnumerable<string> input)
	{
		PrepareStacks(input.First());
		FillStacks(input.Skip(1));
	}

	[MemberNotNull(nameof(stacks))]
	private void PrepareStacks(string stackDescriptionLine)
	{
		var amount = (stackDescriptionLine.Length + 1) / 4;
		stacks = new Stack<char>[amount];
		for(int i = 0; i < amount; i++)
		{
			stacks[i] = new();
		}
	}

	private void FillStacks(IEnumerable<string> stackLayers)
	{
		foreach(var layer in stackLayers)
		{
			AddLayer(layer);
		}
	}

	private void AddLayer(string layer)
	{
		for(int i = 0; i < stacks.Length; i++)
		{
			char stackElement = ExtractElement(layer, i);
			InsertInStack(i, stackElement);
		}
	}

	private char ExtractElement(string layer, int i)
	{
		int extractIndex = (i * 4) + 1;
		return layer[extractIndex];
	}

	private void InsertInStack(int i, char stackElement)
	{
		if(stackElement == ' ')
			return;
		stacks[i].Push(stackElement);
	}

	public Stack<char>[] GetSupplyStacks()
	{
		return stacks;
	}
}
