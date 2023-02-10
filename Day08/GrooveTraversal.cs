using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Helpers;

namespace Day08;

/*
internal class GrooveTraversal
{
	public Action<Action> CurrentAction { get; set}
	public Stack<Coordinate> PositionStack { get; }

	public Coordinate Position => PositionStack.Peek();

	public GrooveTraversal(int x, int y)
	{
		PositionStack = new();
		PositionStack.Push(new(x, y));
	}
}

internal static class GrooveTraversalFluency
{
	public static GrooveTraversal Traversal(this GrooveTraversal groove)
	{
		Action<Action> act = x =>
		{
			x();
		};
		groove.CurrentAction = act;
		return groove;
	}

	public GrooveTraversal SaveState(this GrooveTraversal groove)
	{
		Action<Action> act = x =>
		{
			var current = groove.Position;
			groove.PositionStack.Push(current.Clone());
			x();
			groove.PositionStack.Pop();
		};
		var current = groove.PositionStack.Pop();
		groove.PositionStack.Push(current.Clone());
	}
}
*/
