using Helpers;

namespace Day15.Model;

internal class FormulaNode : INode
{
	public int SideLengthPowerOfTwo { get; }

	// f(x) = Ax + B
	// Upper or lower for which part is Full
	public int? B_PosA_Upper { get; init; }
	public int? B_PosA_Lower { get; init; }
	public int? B_NegA_Upper { get; init; }
	public int? B_NegA_Lower { get; init; }

	public FormulaNode(int sideLengthPowerOfTwo)
	{
		if (sideLengthPowerOfTwo < 1)
			throw new Exception();

		SideLengthPowerOfTwo = sideLengthPowerOfTwo;
	}

	public INode Combine(INode other)
	{
		if (other is SplitNode)
			return Split().Combine(other);

		if (other is not FormulaNode otherForm)
			return other.Combine(this);

		return new FormulaNode(SideLengthPowerOfTwo)
		{
			B_PosA_Upper = Min(B_PosA_Upper, otherForm.B_PosA_Upper),
			B_NegA_Upper = Min(B_NegA_Upper, otherForm.B_NegA_Upper),

			B_PosA_Lower = Max(B_PosA_Lower, otherForm.B_PosA_Lower),
			B_NegA_Lower = Max(B_NegA_Lower, otherForm.B_NegA_Lower),
		};
	}

	private SplitNode Split()
	{
		var newPowerOfTwo = SideLengthPowerOfTwo - 1;
		var newSideLength = 1 << newPowerOfTwo;

		throw new NotImplementedException();
	}

	public bool HasContent(Coordinate coordinate)
	{
		(int x, int y) = ((int, int))coordinate;
		return
			B_PosA_Lower + x >= y ||
			B_PosA_Upper + x <= y ||
			B_NegA_Lower - x >= y ||
			B_NegA_Upper - x <= y;
	}

	private static int? Min(int? a, int? b)
	{
		if (a == null || b == null)
			return a ?? b;

		return a < b ? a : b;
	}

	private static int? Max(int? a, int? b)
	{
		if (a == null || b == null)
			return a ?? b;

		return a > b ? a : b;
	}
}
