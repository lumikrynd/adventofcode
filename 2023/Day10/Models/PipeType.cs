namespace Y2023.Day10.Models;

[Flags]
public enum PipeType
{
	None = 0,
	Up = 1 << 0,
	Down = 1 << 1,
	Left = 1 << 2,
	Right = 1 << 3,

	Start = Up | Down | Left | Right,
	Horizontal = Left | Right,
	Vertical = Up | Down,
	UpToRight = Up | Right,
	UpToLeft = Up | Left,
	DownToRight = Down | Right,
	DownToLeft = Down | Left,
}
