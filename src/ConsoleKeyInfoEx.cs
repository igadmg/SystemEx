using System;

namespace SystemEx
{
	public static class ConsoleKeyInfoEx
	{
		public static void Deconstruct(this ConsoleKeyInfo cki, out ConsoleKey Key)
		{
			Key = cki.Key;
		}
	}
}
