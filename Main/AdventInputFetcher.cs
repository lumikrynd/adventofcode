using System.Net;

namespace Main;

internal static class AdventInputFetcher
{
	public static async Task<IEnumerable<string>> GetInput(int day, int year, string sessionCookie)
	{
		var file = GetCachePath(day, year);
		if (!File.Exists(file))
			await CreateInputFile(day, year, sessionCookie);

		return File.ReadLines(file);
	}

	private static async Task CreateInputFile(int day, int year, string sessionCookie)
	{
		string PuzzleInput = await GetPuzzleInput(day, year, sessionCookie);
		await WriteToFile(day, year, PuzzleInput);
	}

	private static async Task<string> GetPuzzleInput(int day, int year, string sessionCookie)
	{
		var basePage = new Uri(@"https://adventofcode.com");

		CookieContainer cookieContainer = PrepareCookie(basePage, sessionCookie);
		using var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
		using var client = new HttpClient(handler) { BaseAddress = basePage };

		var requestUri = GetRequestUri(day, year);
		var result = await client.GetAsync(requestUri);
		result.EnsureSuccessStatusCode();
		return await result.Content.ReadAsStringAsync();
	}

	private static CookieContainer PrepareCookie(Uri baseAddress, string sessionCookie)
	{
		var session = new Cookie("session", sessionCookie);

		var cookieContainer = new CookieContainer();
		cookieContainer.Add(baseAddress, session);
		return cookieContainer;
	}

	private static async Task WriteToFile(int day, int year, string PuzzleInput)
	{
		var file = GetCachePath(day, year);
		var directory = Path.GetDirectoryName(file) ?? throw new Exception("Wait what");
		Directory.CreateDirectory(directory);
		await File.WriteAllTextAsync(file, PuzzleInput);
	}

	private static string GetRequestUri(int day, int year) => @$"/{year}/day/{day}/input";
	private static string GetCachePath(int day, int year) => @$"Input/{year}_{day:00}.txt";
}