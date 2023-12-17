using System.Text;
using Helpers;

namespace Y2023.Day14.Models;

public class Map
{
	public Map(ParsedInput input)
	{
		RoundRocks = input.RoundRocks;
		SquareRocks = input.SquareRocks.ToHashSet();
		Width = input.Width;
		Height = input.Height;
	}

	public Map(Map previous, List<Coordinate> newRockPositions)
	{
		RoundRocks = newRockPositions;
		SquareRocks = previous.SquareRocks;
		Width = previous.Width;
		Height = previous.Height;
	}

	public List<Coordinate> RoundRocks { get; private init; }
	public HashSet<Coordinate> SquareRocks { get; private init; }
	public int Width { get; private init; }
	public int Height { get; private init; }

	public override bool Equals(object? obj)
	{
		if(obj == null || obj is not Map other)
		{
			return false;
		}

		var aRocks = GetSortedRoundRocks();
		var bRocks = other.GetSortedRoundRocks();

		if(aRocks.Count != bRocks.Count)
			return false;

		return aRocks.Zip(bRocks)
			.All(zip => zip.First == zip.Second);
	}

	private int? hash;
	public override int GetHashCode()
	{
		return hash ??= CalculateHash();
	}

	private int CalculateHash()
	{
		var rocks = GetSortedRoundRocks();

		HashCode hash = new();
		foreach(var rock in rocks)
		{
			hash.Add(rock);
		}
		return hash.ToHashCode();
	}

	private List<Coordinate> GetSortedRoundRocks()
	{
		return RoundRocks
			.OrderBy(r => r.X)
			.ThenBy(r => r.Y)
			.ToList();
	}

	public override string ToString()
	{
		var rocks = RoundRocks.ToHashSet();
		var sb = new StringBuilder();
		for(int y = 0; y < Height; y++)
		{
			for(int x = 0; x < Width; x++)
			{
				var cord = new Coordinate(x, y);
				if(rocks.Contains(cord))
					sb.Append('O');
				else if(SquareRocks.Contains(cord))
					sb.Append('#');
				else
					sb.Append('.');
			}
			sb.AppendLine();
		}
		return sb.ToString();
	}
}