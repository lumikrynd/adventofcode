using System;
using System.Collections.Generic;

namespace Day08;

internal partial class TreeGroove
{
	public int[,] Trees { get; }

	public int Height => Trees.GetLength(0);
	public int Width => Trees.GetLength(1);

	public TreeGroove(int[,] trees)
	{
		Trees = trees;
	}

	public int CountVisibleTrees()
	{
		var visibleTrees = new HashSet<TreeCordinate>();

		for(int h = 0; h < Height; h++)
		{
			int heighestTree = -1;
			for(int w = 0; w < Width && heighestTree < 9; w++)
			{
				var current = Trees[h, w];
				if(current > heighestTree)
				{
					visibleTrees.Add(new TreeCordinate(w, h));
					heighestTree = current;
				}
			}
		}

		for(int h = 0; h < Height; h++)
		{
			int heighestTree = -1;
			for(int w = Width - 1; w >= 0 && heighestTree < 9; w--)
			{
				var current = Trees[h, w];
				if(current > heighestTree)
				{
					visibleTrees.Add(new TreeCordinate(w, h));
					heighestTree = current;
				}
			}
		}

		for(int w = 0; w < Width; w++)
		{
			int heighestTree = -1;
			for(int h = 0; h < Height && heighestTree < 9; h++)
			{
				var current = Trees[h, w];
				if(current > heighestTree)
				{
					visibleTrees.Add(new TreeCordinate(w, h));
					heighestTree = current;
				}
			}
		}

		for(int w = 0; w < Width; w++)
		{
			int heighestTree = -1;
			for(int h = Height - 1; h >= 0 && heighestTree < 9; h--)
			{
				var current = Trees[h, w];
				if(current > heighestTree)
				{
					visibleTrees.Add(new TreeCordinate(w, h));
					heighestTree = current;
				}
			}
		}

		return visibleTrees.Count;
	}

	public int CountVisibleTreesV2()
	{
		var visibleTrees = new HashSet<TreeCordinate>();
		int curHeighest = -1;

		var allWayRound = this.Traverse()
			.From(new(0, 0))
			.ExecuteSerial(
				state => state
					.Increase().X().ToEdge()
					.Freeze().Y()
					.Do(resetHeighest)
					.Increase().Y().While(IsNotMaxHeight),
				state => state
					.Increase().Y().ToEdge()
					.Freeze().X()
					.Do(resetHeighest)
					.Decrease().X().While(IsNotMaxHeight),
				state => state
					.Decrease().X().ToEdge()
					.Freeze().Y()
					.Do(resetHeighest)
					.Decrease().Y().While(IsNotMaxHeight),
				state => state
					.Decrease().Y().ToEdge()
					.Freeze().X()
					.Do(resetHeighest)
					.Increase().X().While(IsNotMaxHeight)
				)
			.If(ShouldAdd)
			.DoAction(AddVisibleTree);
		allWayRound.Execute();

		return visibleTrees.Count;

		bool IsNotMaxHeight(TraversalState state)
		{
			return curHeighest < 9;
		}

		void resetHeighest()
		{
			curHeighest = -1;
		}

		bool ShouldAdd(TraversalState state)
		{
			var treeHeight = Trees[state.Y, state.X];
			return treeHeight > curHeighest;
		}

		void AddVisibleTree(TraversalState state)
		{
			var treeHeight = Trees[state.Y, state.X];
			curHeighest = treeHeight;
			visibleTrees.Add(new TreeCordinate(state.X, state.Y));
		}
	}
}
