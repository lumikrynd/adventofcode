using System.Diagnostics.CodeAnalysis;
using System.Text;
using Helpers;

namespace Day14.Model
{
	internal class CaveMap
	{
		readonly HashSet<Coordinate> Map = new();

		public virtual int Bottom { get; private set; }
		private int LowestX { get; set; }
		private int HighestX { get; set; }

		public CaveMap(List<RockFormation> rocks)
		{
			foreach (var rock in rocks)
			{
				AddRockFormation(rock);
			}
			SetBorders();
		}

		public virtual bool IsFilledOutAt(Coordinate coordinate) => Map.Contains(coordinate);
		public void AddSand(Coordinate coordinate) => Map.Add(coordinate);
		private void AddStone(Coordinate coordinate) => Map.Add(coordinate);

		private void AddRockFormation(RockFormation rock)
		{
			for (int i = 1; i < rock.Count; i++)
			{
				var from = rock[i - 1];
				var to = rock[i];
				FillOutRow(from, to);
			}
		}

		private void FillOutRow(Coordinate from, Coordinate to)
		{
			var direction = GetDirectionVector(from, to);
			var position = from;
			while (!position.Equals(to))
			{
				AddStone(position);
				position = position.AddVector(direction);
			}
			AddStone(position);
		}

		private (int, int) GetDirectionVector(Coordinate from, Coordinate to)
		{
			var (xs, ys) = from.GetVectorTo(to);
			return (Sign(xs), Sign(ys)); //Assumes direction up, down, or vertical.
		}

		private static int Sign(int v)
		{
			return (v > 0) ? 1 : (v < 0) ? -1 : 0;
		}

		[MemberNotNull(nameof(Bottom), nameof(HighestX), nameof(LowestX))]
		private void SetBorders()
		{
			Bottom = Map.Select(c => c.Y).Max();
			HighestX = Map.Select(c => c.X).Max();
			LowestX = Map.Select(c => c.X).Min();
		}

		public string CreateDebugString()
		{
			StringBuilder s = new();
			for (int x = LowestX - 1; x <= HighestX + 1; x++)
			{
				for (int y = 0; y <= Bottom + 1; y++)
				{
					var c = IsFilledOutAt(new(x, y)) ? '#' : '.';
					s.Append(c);
				}
				s.AppendLine();
			}

			return s.ToString();
		}
	}
}
