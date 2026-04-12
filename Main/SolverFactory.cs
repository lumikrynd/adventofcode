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
		switch (year) {
			case 2022: return Y2022(day, out factory);
			case 2023: return Y2023(day, out factory);
			default:
				factory = null;
				return false;
		}
	}

	private static bool Y2022(
		int day,
		[MaybeNullWhen(false)] out Func<IEnumerable<string>, ISolver> factory) {
		switch (day) {
			case 1:
				factory = x => new Y2022.Day01.Challenge(x);
				break;
			default:
				factory = null;
				break;
		}

		return factory is not null;
	}

	private static bool Y2023(
		int day,
		[MaybeNullWhen(false)] out Func<IEnumerable<string>, ISolver> factory) {
		switch (day) {
			case 1:
				factory = x => new Y2023.Day01.Challenge(x);
				break;
			default:
				factory = null;
				return false;
		}

		return true;
	}
}