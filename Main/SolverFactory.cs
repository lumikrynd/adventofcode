using System.Diagnostics.CodeAnalysis;
using Helpers;

namespace Main;

internal static class SolverFactory
{
	public static bool TryGetFactory(
		int year,
		int day,
		[MaybeNullWhen(false)] out Func<IEnumerable<string>, ISolver> factory)
	{
		switch (year)
		{
			case 2022: return Y2022(day, out factory);
			case 2023: return Y2023(day, out factory);
			default:
				factory = null;
				return false;
		}
	}

	private static bool Y2022(
		int day,
		[MaybeNullWhen(false)] out Func<IEnumerable<string>, ISolver> factory)
	{
		factory = day switch
		{
			1 => x => new Y2022.Day01.Challenge(x),
			2 => x => new Y2022.Day02.Challenge(x),
			3 => x => new Y2022.Day03.Challenge(x),
			4 => x => new Y2022.Day04.Challenge(x),
			5 => x => new Y2022.Day05.Challenge(x),
			6 => x => new Y2022.Day06.Challenge(x),
			7 => x => new Y2022.Day07.Challenge(x),
			8 => x => new Y2022.Day08.Challenge(x),
			9 => x => new Y2022.Day09.Challenge(x),
			10 => x => new Y2022.Day10.Challenge(x),
			11 => x => new Y2022.Day11.Challenge(x),
			12 => x => new Y2022.Day12.Challenge(x),
			13 => x => new Y2022.Day13.Challenge(x),
			14 => x => new Y2022.Day14.Challenge(x),
			15 => x => new Y2022.Day15.Challenge(x),
			17 => x => new Y2022.Day17.Challenge(x),
			18 => x => new Y2022.Day18.Challenge(x),
			20 => x => new Y2022.Day20.Challenge(x),
			_ => null,
		};

		return factory is not null;
	}

	private static bool Y2023(
		int day,
		[MaybeNullWhen(false)] out Func<IEnumerable<string>, ISolver> factory)
	{
		factory = day switch
		{
			1 => x => new Y2023.Day01.Challenge(x),
			2 => x => new Y2023.Day02.Challenge(x),
			3 => x => new Y2023.Day03.Challenge(x),
			4 => x => new Y2023.Day04.Challenge(x),
			5 => x => new Y2023.Day05.Challenge(x),
			6 => x => new Y2023.Day06.Challenge(x),
			7 => x => new Y2023.Day07.Challenge(x),
			8 => x => new Y2023.Day08.Challenge(x),
			9 => x => new Y2023.Day09.Challenge(x),
			10 => x => new Y2023.Day10.Challenge(x),
			11 => x => new Y2023.Day11.Challenge(x),
			12 => x => new Y2023.Day12.Challenge(x),
			13 => x => new Y2023.Day13.Challenge(x),
			14 => x => new Y2023.Day14.Challenge(x),
			15 => x => new Y2023.Day15.Challenge(x),
			16 => x => new Y2023.Day16.Challenge(x),
			17 => x => new Y2023.Day17.Challenge(x),
			18 => x => new Y2023.Day18.Challenge(x),
			19 => x => new Y2023.Day19.Challenge(x),
			_ => null,
		};

		return factory is not null;
	}
}