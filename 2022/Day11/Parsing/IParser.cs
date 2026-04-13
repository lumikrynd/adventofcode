namespace Y2022.Day11.Parsing;

public interface IParser<TIn, TOut>
{
	TOut Parse(TIn input);
}