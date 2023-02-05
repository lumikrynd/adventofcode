using System.Collections.Generic;

namespace Day05.Models;

internal interface ICrane
{
	public void MoveBoxes(Stack<char> source, Stack<char> target, int amount);
}