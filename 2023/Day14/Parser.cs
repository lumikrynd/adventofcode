using Helpers;
using Y2023.Day14.Models;

namespace Y2023.Day14;

public class Parser : IDisposable
{
	public static ParsedInput Parse(IEnumerable<string> input)
	{
		using var parser = new Parser(input);
		parser.ParseStones();
		return parser.ToRockPositions();
	}

	List<Coordinate> RoundRocks = new();
	List<Coordinate> SquareRocks = new();

	EnumeratorWrapper<string> Input;
	int Y = 0;
	int Width = 0;

	private Parser(IEnumerable<string> input)
	{
		Input = EnumeratorWrapper<string>.WithInitializedCurrent(input);
		Width = Input.Current.Length;
	}

	public void ParseStones()
	{
		do
		{
			ParseLine(Input.Current);
			Y++;
		}
		while(Input.TryGetNext(out _));
	}

	private void ParseLine(string current)
	{
		for(int x = 0; x < current.Length; x++)
		{
			switch(current[x])
			{
				case '.':
					break;
				case '#':
					SquareRocks.Add(new Coordinate(x, Y));
					break;
				case 'O':
					RoundRocks.Add(new Coordinate(x, Y));
					break;
				default:
					throw new NotImplementedException();
			}
		}
	}

	public ParsedInput ToRockPositions()
	{
		return new()
		{
			RoundRocks = RoundRocks,
			SquareRocks = SquareRocks,
			Height = Y,
			Width = Width,
		};
	}

	public void Dispose()
	{
		Input.Dispose();
	}
}