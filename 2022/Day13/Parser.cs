using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day13.Model;

namespace Day13;

internal class Parser : IDisposable
{
	public static List<Pair> ParseInput(string input)
	{
		using var stream = new Parser(input);

		return stream.ParseInput();
	}

	readonly StringReader Stream;

	private Parser(string input)
	{
		Stream = new StringReader(input);
	}

	public void Dispose()
	{
		Stream.Dispose();
	}

	public List<Pair> ParseInput()
	{
		var result = new List<Pair>();

		while(Stream.Peek() != -1)
		{
			var pair = ReadPair();
			result.Add(pair);

			if (Stream.Peek() != -1)
				ReadEndOfLine();
		}

		return result;
	}

	private Pair ReadPair()
	{
		IValue[] arr = new IValue[2];

		for(int i = 0; i < arr.Length; i++)
		{
			arr[i] = ReadValue();
			ReadEndOfLine();
		}

		return new Pair(arr[0], arr[1]);
	}

	private IValue ReadValue()
	{
		if (Stream.Peek() == '[')
			return ReadValueList();
		else if ((char) Stream.Peek() is >= '0' and <= '9')
			return ReadSingleValue();

		throw new Exception();
	}

	private SingleValue ReadSingleValue()
	{
		int value = 0;
		while((char)Stream.Peek() is >= '0' and <= '9')
		{
			value *= 10;
			value += CharValueToInt(Stream.Read());
		}

		return new SingleValue(value);
	}

	private ValueList ReadValueList()
	{
		if (Stream.Peek() != '[')
			throw new Exception();

		var list = new ValueList();

		Stream.Read();
		do
		{
			if (Stream.Peek() == ']')
				continue;

			var newItem = ReadValue();
			list.Values.Add(newItem);
		}
		while (ContinueReadingList());

		return list;
	}

	private bool ContinueReadingList()
	{
		char item = (char)Stream.Read();

		if (item == ',')
			return true;

		if (item == ']')
			return false;

		throw new Exception();
	}

	private static int CharValueToInt(int charValue) => charValue - '0';

	private void ReadEndOfLine()
	{
		var rest = Stream.ReadLine();
		if (rest != "" && rest != null)
			throw new Exception();
	}
}
