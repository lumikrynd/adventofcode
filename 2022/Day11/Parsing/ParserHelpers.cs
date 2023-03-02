namespace Day11.Parsing;

public static class ParserHelpers
{
	public static string RemoveStart(this string input, string start)
	{
		var temp = input.Trim();
		temp = temp.Substring(start.Length);
		return temp;
	}
}