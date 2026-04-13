using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Main;

internal class Program
{
	static async Task Main(string[] args)
	{
		if (!ParseArgs(args, out var parsed))
			return;
		(var year, var day) = parsed.Value;

		if (!SolverFactory.TryGetFactory(year, day, out var factory)) {
			Console.WriteLine($"No solver for year {year} day {day}");
			return;
		}

		var input = await GetPuzzleInput(year, day);
		var solver = factory(input);

		Console.WriteLine("Solving part 1:");
		Console.WriteLine($"{solver.Part1()}");
		Console.WriteLine();
		Console.WriteLine("Solving part 2:");
		Console.WriteLine($"{solver.Part2()}");
	}

	private static bool ParseArgs(string[] args, [NotNullWhen(true)] out (int, int)? result)
	{
		result = null;

		if (args.Length < 2)
		{
			Console.WriteLine("Need to provide year and day as arguments");
			return false;
		}

		if (!int.TryParse(args[0], out var year))
		{
			Console.WriteLine($"Invalid year: \"{args[0]}\"");
			return false;
		}

		if (!int.TryParse(args[1], out var day))
		{
			Console.WriteLine($"Invalid day: \"{args[1]}\"");
			return false;
		}

		if (year < 100) year += 2000;

		result = (year, day);
		return true;
	}

	private static Task<IEnumerable<string>> GetPuzzleInput(int year, int day)
	{
		var session = GetSessionCookie();
		return AdventInputFetcher.GetInput(day, year, session);
	}

	private static string GetSessionCookie()
	{
		var config = new ConfigurationBuilder()
			.AddUserSecrets<Program>()
			.Build();

		return config["Session"]
			?? throw new Exception("The user secret \"Session\" is not set");
	}
}