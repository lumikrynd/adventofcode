namespace Day17;

internal class Wind
{
	IEnumerator<int> WindDirections;
	public Wind(string input)
	{
		WindDirections = GetWindDirEnumerable(input);
		WindDirections.MoveNext();
	}


	/// <summary>
	/// Get wind direction, -1 meaning left, 1 meaning right.
	/// </summary>
	public int GetWindDir()
	{
		var ret = WindDirections.Current;
		WindDirections.MoveNext();
		return ret;
	}

	public int PeekWindDir()
	{
		return WindDirections.Current;
	}

	/// <summary>
	/// Get wind direction, -1 meaning left, 1 meaning right.
	/// </summary>
	private static IEnumerator<int> GetWindDirEnumerable(string windInput)
	{
		while (true)
		{
			foreach(char c in windInput)
			{
				if (c == '<')
					yield return -1;
				else if (c == '>')
					yield return 1;
				else
					throw new Exception("poluted input string");
			}
		}
	}
}