using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Day08;

internal class TreeGrooveParser
{
	public static TreeGroove Parse(IEnumerable<string> input)
	{
		var parser = new TreeGrooveParser(input.ToArray());
		return parser.Parse();
	}

	int[,] trees;
	string[] input;

	public TreeGrooveParser(string[] input)
	{
		this.input = input;
		InitTrees();
	}

	[MemberNotNull(nameof(trees))]
	private void InitTrees()
	{
		int height = input.Length;
		int width = input.First().Length;
		trees = new int[height, width];
	}

	public TreeGroove Parse()
	{
		for(int h = 0; h < trees.GetLength(0); h++)
		{
			for(int w = 0; w < trees.GetLength(1); w++)
			{
				trees[h, w] = ParseHeight(input[h][w]);
			}
		}
		return new TreeGroove(trees);
	}

	private static int ParseHeight(char input)
	{
		return input switch
		{
			'0' => 0,
			'1' => 1,
			'2' => 2,
			'3' => 3,
			'4' => 4,
			'5' => 5,
			'6' => 6,
			'7' => 7,
			'8' => 8,
			'9' => 9,
			_ => throw new NotImplementedException()
		};
	}
}
