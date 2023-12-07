using System.Diagnostics.CodeAnalysis;
using Y2023.Day05.Models;
using Range = Y2023.Day05.Models.Range;

namespace Y2023.Day05;

public class Converter
{
	readonly Conversion[] Conversions;
	readonly long[] SortedSources;
	readonly Converter? InnerConverter;

	public static Converter FromAlmanac(Almanac almanac)
	{
		var conversions = almanac.Conversions();
		conversions.Reverse();

		Converter? converter = null;
		foreach(var conversion in conversions)
		{
			converter = new Converter(conversion, converter);
		}

		return converter ?? new(new());
	}

	public Converter(List<Conversion> conversions) : this(conversions, null)
	{
	}

	private static Conversion LastConversion = new() { SourceStart = long.MaxValue, DestionationStart = long.MaxValue, Length = 1 };

	private Converter(List<Conversion> conversions, Converter? innerConverter)
	{
		InnerConverter = innerConverter;

		var ordered = conversions
			.OrderBy(x => x.SourceStart)
			.Append(LastConversion)
			.ToArray();

		Conversions = ordered;
		SortedSources = ordered.Select(x => x.SourceStart).ToArray();
	}

	public long Convert(long value)
	{
		var newValue = ConvertSingle(value);
		if(InnerConverter is null)
			return newValue;

		return InnerConverter.Convert(newValue);
	}

	private long ConvertSingle(long value)
	{
		int index = FindIndex(value);

		if(index < 0)
			return value;

		var conversion = Conversions[index];

		if(value > conversion.SourceEnd)
			return value;

		return Convert(value, conversion);
	}

	public List<Range> ConvertRanges(List<Range> ranges)
	{
		List<Range> converted = ranges
			.SelectMany(ConvertRange)
			.ToList();

		if(InnerConverter is null)
			return converted;

		return InnerConverter.ConvertRanges(converted);
	}

	private List<Range> ConvertRange(Range range)
	{
		var currentRange = range;
		List<Range> converted = new();

		AddInitialRange(converted, ref currentRange);

		while(currentRange != null)
		{
			AddNextRange(converted, ref currentRange);
		}

		return converted;
	}

	private void AddInitialRange(List<Range> converted, [DisallowNull] ref Range? range)
	{
		var startIndex = FindIndex(range.Start);
		if(startIndex < 0)
			return;

		var conversion = Conversions[startIndex];

		(var split, range) = SplitRange(range, conversion.SourceEnd + 1);

		if(split is null)
			return;

		var newStart = Convert(split.Start, conversion);
		var @new = Range.FromLength(newStart, split.Length);
		converted.Add(@new);
	}

	private void AddNextRange(List<Range> converted, [DisallowNull] ref Range? range)
	{
		var index = FindNextIndex(range.Start);
		var conversion = Conversions[index];

		(var split, range) = SplitRange(range, conversion.SourceStart);

		if(split != null)
			converted.Add(split);

		if(range is null)
			return;

		(split, range) = SplitRange(range, conversion.SourceStart + conversion.Length);

		var newStart = Convert(split!.Start, conversion);
		var @new = Range.FromLength(newStart, split.Length);
		converted.Add(@new);
	}

	private (Range?, Range?) SplitRange(Range range, long startIndex)
	{
		if(range.Start >= startIndex)
			return (null, range);
		if(range.End < startIndex)
			return (range, null);

		var start = Range.FromEnd(range.Start, startIndex - 1);
		var end = Range.FromEnd(startIndex, range.End);
		return (start, end);
	}

	private int FindIndex(long value)
	{
		var index = Array.BinarySearch(SortedSources, value);
		if(index < 0)
		{
			index = ~index;
			index -= 1;
		}

		return index;
	}

	private int FindNextIndex(long value)
	{
		var index = Array.BinarySearch(SortedSources, value);
		if(index < 0)
			index = ~index;

		return index;
	}

	private long Convert(long value, Conversion conversion) =>
		value - conversion.SourceStart + conversion.DestionationStart;
}