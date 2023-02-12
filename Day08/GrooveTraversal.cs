using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Helpers;

namespace Day08;

public class TraversalState
{
	private readonly TraversalStep firstStep;
	public TraversalStep OuterStep { get; set; }

	public int X { get; set; }
	public int Y { get; set; }

	public int XLength { get; }
	public int YLength { get; }

	public TraversalState(int xLength, int yLength)
	{
		XLength = xLength;
		YLength = yLength;
		firstStep = new();
		OuterStep = firstStep;
	}

	public void Execute() => firstStep.Task();
}

public class TraversalStep
{
	public Action Task { get; set; } = () => { };
}

internal static class TraversalFluency
{
	public static TraversalState Traverse(this TreeGroove groove)
	{
		var xMax = groove.Width;
		var yMax = groove.Height;
		return new TraversalState(xMax, yMax);
	}

	public static TraversalState Traverse(this TreeGroove groove, Coordinate coord)
	{
		var state = groove.Traverse();
		return state.From(coord);
	}

	public static TraversalState DoAction(this TraversalState state, Action act)
	{
		return state.DoAction(_ => act());
	}

	public static TraversalState DoAction(this TraversalState state, Action<TraversalState> act)
	{
		var newOuter = new TraversalStep();
		void newAction()
		{
			act(state);
			newOuter.Task();
		}

		return state.InjectAction(newAction, newOuter);
	}

	public static TraversalState If(this TraversalState state, Func<TraversalState, bool> condition)
	{
		var newOuter = new TraversalStep();
		void newAction()
		{
			if(condition(state))
			{
				newOuter.Task();
			}
		}

		return state.InjectAction(newAction, newOuter);
	}

	public static TraversalState From(this TraversalState state, Coordinate coord)
	{
		var newOuter = new TraversalStep();
		void newAction()
		{
			state.X = coord.X;
			state.Y = coord.Y;
			newOuter.Task();
		}

		return state.InjectAction(newAction, newOuter);
	}

	public static TraversalState IncreaseXToEdge(this TraversalState state)
	{
		return state.IncreaseXWhile(_ => true);
	}

	public static TraversalState IncreaseYToEdge(this TraversalState state)
	{
		return state.IncreaseYWhile(_ => true);
	}

	public static TraversalState IncreaseXWhile(this TraversalState state, Func<TraversalState, bool> @while)
	{
		void set(int x) => state.X = x;
		return state.IncreaseWhile(() => state.X, @while, set, state.XLength); ;
	}

	public static TraversalState IncreaseYWhile(this TraversalState state, Func<TraversalState, bool> @while)
	{
		void set(int y) => state.Y = y;
		return state.IncreaseWhile(() => state.Y, @while, set, state.YLength);
	}

	private static TraversalState IncreaseWhile(this TraversalState state, Func<int> start, Func<TraversalState, bool> @while, Action<int> set, int length)
	{
		static int increase(int p) => p + 1;
		return state.TraverseWhile(@while, start, set, increase, length);
	}

	public static TraversalState DecreaseXToBorder(this TraversalState state)
	{
		return state.DecreaseXWhile(_ => true);
	}

	public static TraversalState DecreaseYToBorder(this TraversalState state)
	{
		return state.DecreaseYWhile(_ => true);
	}

	public static TraversalState DecreaseXWhile(this TraversalState state, Func<TraversalState, bool> @while)
	{
		void set(int x) => state.X = x;
		return state.DecreaseWhile(() => state.X, @while, set, state.XLength); ;
	}

	public static TraversalState DecreaseYWhile(this TraversalState state, Func<TraversalState, bool> @while)
	{
		void set(int y) => state.Y = y;
		return state.DecreaseWhile(() => state.Y, @while, set, state.YLength);
	}

	private static TraversalState DecreaseWhile(this TraversalState state, Func<int> start, Func<TraversalState, bool> @while, Action<int> set, int length)
	{
		static int Decrease(int p) => p - 1;
		return state.TraverseWhile(@while, start, set, Decrease, length);
	}

	private static TraversalState TraverseWhile(this TraversalState state, Func<TraversalState, bool> @while, Func<int> start, Action<int> set, Func<int, int> step, int length)
	{
		var newOuter = new TraversalStep();
		void newAction()
		{
			for(int p = start(); @while(state) && p.InsideBounds(0, length); p = step(p))
			{
				set(p);
				newOuter.Task();
			}
		}

		return state.InjectAction(newAction, newOuter);
	}

	private static bool InsideBounds(this int value, int lower, int upper)
	{
		return value >= lower && value < upper;
	}

	public static TraversalState FreezeCoordinate(this TraversalState state)
	{
		return state.FreezeX().FreezeY();
	}

	public static TraversalState FreezeX(this TraversalState state)
	{
		void set(int x) => state.X = x;
		return state.Freeze(() => state.X, set); ;
	}

	public static TraversalState FreezeY(this TraversalState state)
	{
		void set(int y) => state.Y = y;
		return state.Freeze(() => state.Y, set); ;
	}

	private static TraversalState Freeze(this TraversalState state, Func<int> get, Action<int> set)
	{
		var newOuter = new TraversalStep();
		void newAction()
		{
			int initial = get();
			newOuter.Task();
			set(initial);
		}

		return state.InjectAction(newAction, newOuter);
	}

	private static TraversalState InjectAction(this TraversalState state, Action newAction, TraversalStep newOuter)
	{
		state.OuterStep.Task = newAction;
		state.OuterStep = newOuter;
		return state;
	}
}
