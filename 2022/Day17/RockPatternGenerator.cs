namespace Day17;

/// <summary>
/// The binary representation of a rock is mirrored and upside down compared to the drawing.
/// The reason is higher value equals farther from left.
/// Also small index means bottom of rock
/// </summary>
internal class RockPatternGenerator
{
	private IEnumerator<int[]> RockSequence { get; set; }

	public RockPatternGenerator()
	{
		RockSequence = CreateRockSequence();
		RockSequence.MoveNext();
	}

	public int[] Peek()
	{
		return RockSequence.Current;
	}

	public int[] GetNext()
	{
		var temp = RockSequence.Current;
		RockSequence.MoveNext();
		return temp;
	}

	private static IEnumerator<int[]> CreateRockSequence()
	{
		while (true)
		{
			yield return Get_S1();
			yield return Get_S2();
			yield return Get_S3();
			yield return Get_S4();
			yield return Get_S5();
		}
	}

	public static int[] Get_S1()
	{
		return new int[]
		{
			0b1111
		};
	}

	public static int[] Get_S2()
	{
		return new int[]
		{
			0b010,
			0b111,
			0b010,
		};
	}

	public static int[] Get_S3()
	{
		return new int[]
		{
			0b111,
			0b100,
			0b100,
		};
	}

	public static int[] Get_S4()
	{
		return new int[]
		{
			0b1,
			0b1,
			0b1,
			0b1,
		};
	}

	public static int[] Get_S5()
	{
		return new int[]
		{
			0b11,
			0b11,
		};
	}
}
