namespace Day11.Parsing;

public class ExpressionParser : IParser<string, Func<long, long>>
{
	public Func<long, long> Parse(string input)
	{
		var parts = input.Split();
		var leftArg = ParseArgument(parts[0]);
		var operation = ParseOperation(parts[1]);
		var rightArg = ParseArgument(parts[2]);
		return BuildExpression(leftArg, operation, rightArg);
	}

	private Func<long, long> BuildExpression(Func<long, long> leftArg, Func<long, long, long> operation, Func<long, long> rightArg) =>
		x => operation(leftArg(x), rightArg(x));

	private static Func<long, long> ParseArgument(string arg)
	{
		if (arg == "old")
			return x => x;
		long result = long.Parse(arg);
		return x => result;
	}

	private Func<long, long, long> ParseOperation(string operation)
	{
		if (operation == "+")
			return (x, y) => x + y;
		if (operation == "*")
			return (x, y) => x * y;

		throw new NotImplementedException();
	}
}