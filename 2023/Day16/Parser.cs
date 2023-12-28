using Helpers;
using Y2023.Day16.Models;

namespace Y2023.Day16;

public class Parser : IDisposable
{
	public static Map Parse(IEnumerable<string> input)
	{
		using var parser = new Parser(input);
		parser.Parse();
		return parser.CreateMap();
	}

	private Map CreateMap()
	{
		return new(Mirrors, Width, Y);
	}

	private List<Mirror> Mirrors = new();

	private EnumeratorWrapper<string> Input;
	int Y = 0;
	int Width;

	private Parser(IEnumerable<string> input)
	{
		Input = EnumeratorWrapper<string>.WithInitializedCurrent(input);
		Width = Input.Current.Length;
	}

	public void Parse()
	{
		do
		{
			ParseLine(Input.Current);
			Y++;
		}
		while(Input.TryGetNext(out _));
	}

	public void ParseLine(string line)
	{
		for(int x = 0; x < line.Length; x++)
		{
			var mirrorType = ParseMirrorType(line[x]);
			if(mirrorType != null)
				Mirrors.Add(new(new(x, Y), mirrorType.Value));
		}
	}

	private MirrorType? ParseMirrorType(char v) => v switch
	{
		'.' => null,
		'|' => MirrorType.Vertical,
		'-' => MirrorType.Horizontal,
		'\\' => MirrorType.Main,
		'/' => MirrorType.Anti,
		_ => throw new NotImplementedException(),
	};

	public void Dispose()
	{
		Input.Dispose();
	}
}