using System.Collections.Generic;

namespace Day05.Models;

internal class OldCrane : ICrane
{
	public void MoveBoxes(Stack<char> source, Stack<char> target, int amount)
	{
		for(int i = 0; i < amount && source.Count > 0; i++)
		{
			MoveBox(source, target);
		}
	}

	private void MoveBox(Stack<char> source, Stack<char> target)
	{
		var box = source.Pop();
		target.Push(box);
	}
}
