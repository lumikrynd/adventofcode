using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers;

public static class CoordinateHelpers
{
	public static int ManhattenDistance(this (int x, int y) a, (int x, int y) b)
	{
		return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
	}

	/// <summary>
	/// Vector to "to" from "from".
	/// </summary>
	public static (int x, int y) RelativeTo(this (int x, int y) to, (int x, int y) from)
	{
		return (to.x - from.x, to.y - from.y);
	}
}
