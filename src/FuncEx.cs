using System;

namespace SystemEx
{
	public static class FuncEx
	{
		public static Func<T> ToFunc<T>(this T v)
			=> () => v;
	}
}
