using System.Collections.Generic;

namespace Day05.Models;

internal class StackCrane : ICrane
{
	public void MoveBoxes(Stack<char> source, Stack<char> target, int amount)
	{
		var cargo = TakeOutCrates(source, amount);
		InsertCrates(target, cargo);
	}

	private IEnumerable<char> TakeOutCrates(Stack<char> source, int amount)
	{
		var result = new Stack<char>();
		for(int i = 0; i < amount && source.Count > 0; i++)
		{
			var item = source.Pop();
			result.Push(item);
		}
		return result;
	}

	private void InsertCrates(Stack<char> targetStack, IEnumerable<char> crates)
	{
		foreach(var crate in crates)
		{
			targetStack.Push(crate);
		}
	}
}
