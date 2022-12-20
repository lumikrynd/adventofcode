namespace Day02;

internal class Game
{
	public Move Player { get; init; }
	public Move Opponent { get; init; }

	public Game(Move player, Move opponent)
	{
		Player = player;
		Opponent = opponent;
	}

	public Game(Result result, Move opponent)
	{
		Opponent = opponent;

		Player = (Move)(((int)Opponent + 3 + (int)result) % 3);
	}

	public int Score()
	{
		var result = 0;
		result += (((int)Opponent + 1) % 3) == (int)Player ? 6 : 0;
		result += Opponent == Player ? 3 : 0;
		result += (int)Player + 1;

		return result;
	}

	public override string ToString()
	{
		return $"{Score()} : {Player} vs {Opponent}";
	}
}

internal enum Move
{
	Rock = 0,
	Paper = 1,
	Scissor = 2,
}

internal enum Result
{
	Loose = -1, //X
	Draw = 0, //Y
	Win = 1, //Z
}
