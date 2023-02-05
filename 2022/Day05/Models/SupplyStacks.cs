using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day05.Models;

internal class SupplyStacks
{
	private Stack<char>[] stacks;
	private ICrane crane;

	public SupplyStacks(ICrane crane, IEnumerable<char>[] stacks)
	{
		this.crane = crane;
		this.stacks = new Stack<char>[stacks.Length];

		for(int i = 0; i < stacks.Length; i++)
		{
			this.stacks[i] = new Stack<char>(stacks[i].Reverse());
		}
	}

	public char PeekStack(int index)
	{
		var stack = GetStack(index);
		return stack.Peek();
	}

	public void Move(Move move)
	{
		var source = GetStack(move.From);
		var target = GetStack(move.To);
		crane.MoveBoxes(source, target, move.Amount);
	}

	private Stack<char> GetStack(int index)
	{
		return stacks[index - 1];
	}

	public int StackCount => stacks.Length;

	public override string ToString()
	{
		StringBuilder stringBuilder = new();
		foreach(var stack in stacks)
		{
			var temp = StackString(stack);
			stringBuilder.AppendLine(temp);
		}
		return stringBuilder.ToString();
	}

	private string StackString(Stack<char> stack)
	{
		return string.Join(' ', stack.Reverse());
	}
}
