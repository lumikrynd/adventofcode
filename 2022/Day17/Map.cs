using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
	internal class Map
	{
		private readonly List<int> StoppedRocks = new();

		public Map()
		{
		}

		public bool CheckForCollision(Rock rock)
		{
			for (int i = 0; i < rock.Length && i + rock.Altitude < StoppedRocks.Count; i++)
			{
				if ((rock[i] & StoppedRocks[rock.Altitude + i]) != 0)
					return true;
			}

			return false;
		}

		public void PlaceRock(Rock rock)
		{
			if (rock.Altitude > StoppedRocks.Count)
				throw new Exception();

			for(int i = StoppedRocks.Count; i < rock.Length + rock.Altitude; i++)
			{
				StoppedRocks.Add(0);
			}

			for(int i = 0; i < rock.Length; i++)
			{
				StoppedRocks[rock.Altitude + i] |= rock[i];
			}
		}

		internal int Height() => StoppedRocks.Count;

		public string PrintMap()
		{
			var sw = new StringWriter();
			sw.WriteLine("#########");
			foreach(var rock in StoppedRocks)
			{
				sw.WriteLine($"#{GetBit(rock, 0)}{GetBit(rock, 1)}{GetBit(rock, 2)}{GetBit(rock, 3)}{GetBit(rock, 4)}{GetBit(rock, 5)}{GetBit(rock, 6)}#");
			}

			return sw.ToString();
		}

		public static char GetBit(int row, int bit)
		{
			if ((row & (1 << bit)) != 0)
				return 'o';
			return '.';
		}
	}
}
