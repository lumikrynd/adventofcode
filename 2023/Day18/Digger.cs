using Helpers;
using Y2023.Day18.Models;

namespace Y2023.Day18;

using DS = (long Length, bool PrimaryDir);
using DigResult = (long inner, long edgeDiff);
public class Digger
{
	public static long CalculateLavaArea(List<RelativeDigStep> digPlan)
	{
		Digger digger = new(digPlan);
		digger.Dig();
		return digger.DugArea();
	}


	private CircularList<DS> _remainingDigPlan;
	private CircularList<DS>.Node _current;

	private long _dugArea = 0;
	private int _remainingPlanLength;

	private Digger(List<RelativeDigStep> digPlan)
	{
		var primaryTurn = GetPrimaryTurn(digPlan);

		_remainingDigPlan = digPlan
			.Select(s => ((long)s.Length, s.Turn == primaryTurn))
			.ToCircularList();

		_current = _remainingDigPlan.First();
		_remainingPlanLength = _remainingDigPlan.Count();
	}

	private static Turn GetPrimaryTurn(IEnumerable<RelativeDigStep> digPlan)
	{
		int rightTurns = digPlan.Count(s => s.Turn == Turn.Right);
		int leftTurns = digPlan.Count(s => s.Turn == Turn.Left);

		Turn majority = rightTurns > leftTurns ? Turn.Right : Turn.Left;
		return majority;
	}

	public void Dig()
	{
		while(_remainingPlanLength > 4)
		{
			bool success = TryDoDigstep();
			if(!success)
				_current = _current.Next;
		}
		if(_remainingPlanLength == 4)
		{
			var width = _current.Item.Length;
			var height = _current.Next.Item.Length;
			_dugArea += (width + 1) * (height + 1);
			_remainingPlanLength = 0;
		}
	}

	private bool TryDoDigstep()
	{
		var lookAt = _current
			.EnumerateFrom()
			.Select(n => n.Item)
			.Take(5)
			.ToList();

		var pattern = lookAt
			.Select(s => s.PrimaryDir)
			.ToList();

		if(DigBump_LowerAfter(lookAt, pattern))
			return true;

		if(DigBump_SameStartHeight(lookAt, pattern))
			return true;

		if(DigCorner(lookAt, pattern))
			return true;

		return false;
	}

	private bool DigBump_LowerAfter(List<DS> lookAt, List<bool> turnPattern)
	{
		if(lookAt[3].Length <= lookAt[1].Length)
			return false;

		if(turnPattern is [false, true, true, ..])
		{
			var result = DigBump_LowerAfter(lookAt);
			_dugArea += result.inner + result.edgeDiff;
			return true;
		}

		if(turnPattern is [true, false, false, ..])
		{
			var result = DigBump_LowerAfter(lookAt);
			_dugArea -= result.inner;
			return true;
		}

		return false;
	}

	private DigResult DigBump_LowerAfter(List<DS> lookAt)
	{
		long bumpHeight = lookAt[1].Length;
		long bumpWidth = lookAt[2].Length;

		_current.Next.Remove();
		_current.Next.Remove();
		_remainingPlanLength -= 2;

		DS newCurrentVal = (lookAt[0].Length + bumpWidth, !lookAt[0].PrimaryDir);
		_current = _current.ReplaceWith(newCurrentVal);

		var newNodeItem = (lookAt[3].Length - bumpHeight, lookAt[3].PrimaryDir);
		_current.Next.ReplaceWith(newNodeItem);

		return BumpResult(bumpHeight, bumpWidth);
	}

	private bool DigBump_SameStartHeight(List<DS> lookAt, List<bool> turnPattern)
	{
		if(lookAt[1].Length != lookAt[3].Length)
			return false;

		if(turnPattern is [false, true, true, false, ..])
		{
			var result = DigBump_SameStartHeight(lookAt);
			_dugArea += result.inner + result.edgeDiff;
			return true;
		}

		if(turnPattern is [true, false, false, true, ..])
		{
			var result = DigBump_SameStartHeight(lookAt);
			_dugArea -= result.inner;
			return true;
		}

		return false;
	}

	private DigResult DigBump_SameStartHeight(List<DS> lookAt)
	{
		long bumpHeight = lookAt[1].Length;
		long bumpWidth = lookAt[2].Length;

		_current.Next.Remove();
		_current.Next.Remove();
		_current.Next.Remove();
		_current.Next.Remove();
		_remainingPlanLength -= 4;

		DS newCurrentVal = (lookAt[0].Length + bumpWidth + lookAt[4].Length, lookAt[4].PrimaryDir);
		_current = _current.ReplaceWith(newCurrentVal);

		return BumpResult(bumpHeight, bumpWidth);
	}

	private DigResult BumpResult(long bumpHeight, long bumpWidth)
	{
		long innerArea = bumpHeight * (bumpWidth - 1);
		long edgeDiff = 2 * bumpHeight;
		return (innerArea, edgeDiff);
	}

	private bool DigCorner(List<DS> lookAt, List<bool> turnPattern)
	{
		if(turnPattern is [false, true, false, ..])
		{
			_dugArea += DigCorner(lookAt);
			return true;
		}
		else if (turnPattern is [true, false, true, ..])
		{
			_dugArea -= DigCorner(lookAt);
			return true;
		}

		return false;
	}

	private long DigCorner(List<DS> lookAt)
	{
		long bumpHeight = lookAt[1].Length;
		long bumpWidth = lookAt[2].Length;

		_current.Next.Remove();
		_current.Next.Remove();
		_remainingPlanLength -= 2;

		DS newCurrentVal = (lookAt[0].Length + bumpWidth, lookAt[0].PrimaryDir);
		_current = _current.ReplaceWith(newCurrentVal);

		DS newNodeItem = (lookAt[3].Length + bumpHeight, lookAt[3].PrimaryDir);
		_current.Next.ReplaceWith(newNodeItem);

		return (long)bumpHeight * bumpWidth;
	}

	public long DugArea()
	{
		return _dugArea;
	}
}