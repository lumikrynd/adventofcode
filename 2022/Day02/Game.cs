namespace Day02;

internal class Game
{
	public Move Player { get; init; }
	public Move Opponent { get; init; }
	public Result Result { get; init; }

	public Game(Move player, Move opponent)
	{
		Player = player;
		Opponent = opponent;
		Result = CalculateResult(player, opponent);
	}

	private Result CalculateResult(Move player, Move opponent)
	{
		if(player == opponent)
			return Result.Draw;

		if(player == CalculateMove(Result.Win, opponent))
			return Result.Win;

		if(player == CalculateMove(Result.Loose, opponent))
			return Result.Loose;

		throw new Exception();
	}

	public Game(Result result, Move opponent)
	{
		Opponent = opponent;
		Result= result;
		Player = CalculateMove(result, opponent);
	}

	private static Move CalculateMove(Result result, Move opponent)
	{
		int move = (int)opponent + (int)result;
		move %= 3;
		return (Move)move;
	}

	public int Score()
	{
		var score = 0;
		score += ShapeScore();
		score += ResultScore();
		return score;
	}

	private int ShapeScore()
	{
		return (int)Player + 1;
	}

	private int ResultScore()
	{
		return Result switch
		{
			Result.Draw => 3,
			Result.Win => 6,
			Result.Loose => 0,
			_ => throw new NotImplementedException(),
		};
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
	Draw = 0, //Y
	Win = 1, //Z
	Loose = 2, //X
}
