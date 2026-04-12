using Helpers;

namespace Y2023.Day01;

public class Challenge(IEnumerable<string> input) : ISolver
{
	public string Part1()
	{
		var sum = 0;
		foreach(var line in input)
		{
			char a = line.First(x => x <= '9' && x >= '0');
			char b = line.Last(x => x <= '9' && x >= '0');
			var number = $"{a}{b}";
			sum += int.Parse(number);
		}

		return sum.ToString();
	}

	public string Part2()
	{
		var sum = 0;
		foreach(var line in input)
		{
			char a = FirstDigit(line);
			char b = LastDigit(line);
			var number = $"{a}{b}";
			sum += int.Parse(number);
		}

		return sum.ToString();
	}

	char FirstDigit(string input)
	{
		int position = int.MaxValue;
		char result = '0';
		foreach(var item in Substrings)
		{
			var index = input.IndexOf(item.substring);
			if(index != -1 && index < position)
			{
				result = item.value;
				position = index;
			}
		}

		return result;
	}

	char LastDigit(string input)
	{
		int position = -1;
		char result = '0';
		foreach(var item in Substrings)
		{
			var index = input.LastIndexOf(item.substring);
			if(index > position)
			{
				result = item.value;
				position = index;
			}
		}

		return result;
	}

	(string substring, char value)[] Substrings = new[]
	{
		("0", '0'), ("zero", '0'),
		("1", '1'), ("one", '1'),
		("2", '2'), ("two", '2'),
		("3", '3'), ("three", '3'),
		("4", '4'), ("four", '4'),
		("5", '5'), ("five", '5'),
		("6", '6'), ("six", '6'),
		("7", '7'), ("seven", '7'),
		("8", '8'), ("eight", '8'),
		("9", '9'), ("nine", '9'),
	};
}