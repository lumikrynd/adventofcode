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
		factory = null;
		return false;
	}
}