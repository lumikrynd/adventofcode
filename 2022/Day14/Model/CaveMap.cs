using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14.Model
{
	internal class CaveMap
	{
		private Dictionary<int, HashSet<int>> Map = new();

		public int Bottom { get; private set; } = 0;
		public int LowestX { get; private set; } = int.MaxValue;
		public int HighestX { get; private set; } = int.MinValue;

		public CaveMap(List<Rock> rocks)
		{
			foreach (var rock in rocks)
			{
				AddRock(rock);
			}
		}

		private void AddRock(Rock rock)
		{
			var previous = rock.First();
			AddSingleRock(previous.x, previous.y);

			for (int i = 1; i < rock.Count; i++)
			{
				var next = rock[i];
				FillOutSpaceBetween(previous, next);
			}
		}

		private void FillOutSpaceBetween((int x, int y) from, (int x, int y) to)
		{
			var xs = SingleDimensionDirection(from.x, to.x);
			var ys = SingleDimensionDirection(from.y, to.y);

			var position = from;

			while (position != to)
			{
				position = (position.x + xs, position.y + ys);
				AddSingleRock(position.x, position.y);
			}
		}

		private static int SingleDimensionDirection(int from, int to)
		{
			var v = to - from; //vector
			return (v > 0) ? 1 : (v < 0) ? -1 : 0;
		}

		private void AddSingleRock(int x, int y)
		{
			Add(x, y);

			if (y > Bottom)
				Bottom = y;

			if (x > HighestX)
				HighestX = x;

			if (x < LowestX)
				LowestX = x;
		}

		public bool Contains(int x, int y)
		{
			if (!Map.TryGetValue(x, out var inner))
			{
				return false;
			}

			return inner.Contains(y);
		}

		public bool Add(int x, int y)
		{
			HashSet<int> inner;
			if (!Map.TryGetValue(x, out inner))
			{
				inner = new HashSet<int>();
				Map.Add(x, inner);
			}

			return inner.Add(y);
		}

		public string CreateDebugString()
		{
			StringBuilder s = new();
			for (int x = LowestX - 1; x <= HighestX + 1; x++)
			{
				for (int y = 0; y <= Bottom + 1; y++)
				{
					var c = Contains(x, y) ? '#' : '.';
					s.Append(c);
				}
				s.AppendLine();
			}

			return s.ToString();
		}
	}
}
