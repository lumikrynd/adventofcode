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

public class NeedProperty<T>
{
	public Func<Func<int>, Action<int>, int, T> Creator { get; }
	public TraversalState State { get; }

	public NeedProperty(TraversalState state, Func<Func<int>, Action<int>, int, T> propInjector)
	{
		State = state;
		Creator = propInjector;
	}

	public T Inject(Func<int> get, Action<int> set, int length)
	{
		return Creator(get, set, length);
	}
}

public class TraversalNeedLimit
{
	public TraversalState State { get; }
	public Func<int> Get { get; }
	public Action<int> Set { get; }
	public int Length { get; }
	public Func<int, int> Changer { get; }

	public TraversalNeedLimit(TraversalState state, Func<int> get, Action<int> set, int length, Func<int, int> step)
	{
		State = state;
		Get = get;
		Set = set;
		Length = length;
		Changer = step;
	}
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
		return groove.Traverse().From(coord);
	}

	public static TraversalState Do(this TraversalState state, Action act)
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

	public static NeedProperty<TraversalNeedLimit> Increase(this TraversalState state)
	{
		return state.Change(p => p + 1);
	}

	public static NeedProperty<TraversalNeedLimit> Decrease(this TraversalState state)
	{
		return state.Change(p => p - 1);
	}

	private static NeedProperty<TraversalNeedLimit> Change(this TraversalState state, Func<int, int> step)
	{
		TraversalNeedLimit Create(Func<int> get, Action<int> set, int length)
		{
			return new TraversalNeedLimit(state, get, set, length, step);
		}
		return new(state, Create);
	}

	public static T X<T>(this NeedProperty<T> command)
	{
		var state = command.State;
		int Get() => state.X;
		void Set(int value) => state.X = value;
		return command.Inject(Get, Set, state.XLength);
	}

	public static T Y<T>(this NeedProperty<T> command)
	{
		var state = command.State;
		int Get() => state.Y;
		void Set(int value) => state.Y = value;
		return command.Inject(Get, Set, state.YLength);
	}

	public static TraversalState While(this TraversalNeedLimit traversal, Func<TraversalState, bool> @while)
	{
		return traversal.State.TraverseWhile(@while, traversal.Get, traversal.Set, traversal.Changer, traversal.Length);
	}

	public static TraversalState ToEdge(this TraversalNeedLimit traversal)
	{
		return traversal.While(_ => true);
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
		return state.Freeze().X().Freeze().Y();
	}

	public static NeedProperty<TraversalState> Freeze(this TraversalState state)
	{
		TraversalState Create(Func<int> get, Action<int> set, int length)
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
		return new(state, Create);
	}

	public static TraversalState ExecuteSerial(this TraversalState state, params Func<TraversalState, TraversalState>[] actions)
	{
		var todo = new List<Action>();
		void newAction()
		{
			foreach(var act in todo)
			{
				act();
			}
		}

		var newOuter = new TraversalStep();
		var originalOuterStep = state.OuterStep;

		foreach(var action in actions)
		{
			action(state);
			todo.Add(originalOuterStep.Task);
			state.OuterStep.Task = () => newOuter.Task();
			state.OuterStep = originalOuterStep;
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
