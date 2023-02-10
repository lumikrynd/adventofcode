using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08;

internal partial class TreeGroove
{
	public int[,] Trees { get; }

	int Height => Trees.GetLength(0);
	int Width => Trees.GetLength(1);

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
}
