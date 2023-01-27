namespace Helpers
{
	public static class Bits
	{
		//https://graphics.stanford.edu/~seander/bithacks.html#RoundUpPowerOf2
		public static int RoundUpPowerOfTwo(int n)
		{
			n--;
			n |= n >> 1;
			n |= n >> 2;
			n |= n >> 4;
			n |= n >> 8;
			n |= n >> 16;
			n++;

			return n;
		}

		// https://graphics.stanford.edu/~seander/bithacks.html#IntegerLog
		public static int LogBase2(int n)
		{
			int r, shift;
			r = (n > 0xFFFF) ? 1 << 4 : 0;
			n >>= r;

			shift = (n > 0xFF) ? 1 << 3 : 0;
			n >>= shift;
			r |= shift;

			shift = (n > 0xF) ? 1 << 2 : 0;
			n >>= shift;
			r |= shift;

			shift = (n > 0x3) ? 1 << 1 : 0;
			n >>= shift;
			r |= shift;

			r |= (n >> 1);

			return r;
		}

		public static int TwoPower(int n)
		{
			return 1 << n;
		}

		public static bool IsSingleBit(int n, bool excludeZero = false)
		{
			return
				((n & (n - 1)) == 0) &&
				(!excludeZero || n > 0);
		}
	}
}