namespace Y2023.Day19.Models;

public record PartRange(CatRange X, CatRange M, CatRange A, CatRange S)
{
	public long Combinations => 1L * X.Length * M.Length * A.Length * S.Length;

	public CatRange GetCategoryRange(Category category) => category switch
	{
		Category.X => X,
		Category.M => M,
		Category.A => A,
		Category.S => S,
		_ => throw new NotImplementedException(),
	};

	/// <summary>
	/// Split the range at the value. Note <paramref name="value"/> will be the lowest value of the biggest of the 2 ranges.
	/// </summary>
	public (PartRange? Under, PartRange? Over) SplitAt(Category category, int value)
	{
		var range = GetCategoryRange(category);
		var ranges = range.SplitAt(value);

		var under = ranges.Under is null ? null : PartialClone(category, ranges.Under);
		var over = ranges.Over is null ? null : PartialClone(category, ranges.Over);
		return (under, over);
	}

	private PartRange PartialClone(Category category, CatRange range)
	{
		var x = category == Category.X ? range : X;
		var m = category == Category.M ? range : M;
		var a = category == Category.A ? range : A;
		var s = category == Category.S ? range : S;

		return new PartRange(x, m, a, s);
	}
}

/// <summary>
/// Short for Category Range
/// </summary>
public record CatRange(int Min, int Max)
{
	public int Length => Max - Min + 1;

	/// <summary>
	/// Split the range at the value. Note <paramref name="value"/> will be the lowest value of the biggest of the 2 ranges.
	/// </summary>
	public (CatRange? Under, CatRange? Over) SplitAt(int value)
	{
		if(Max < value)
			return (this, null);
		if(Min >= value)
			return (null, this);

		var minRange = new CatRange(Min, value - 1);
		var maxRange = new CatRange(value, Max);
		return (minRange, maxRange);
	}
};
