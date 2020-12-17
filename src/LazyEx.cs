using System;

namespace SystemEx
{
	public static class LazyEx
	{
		public static void Dispose<T>(this Lazy<T> l)
			where T : IDisposable
		{
			if (l.IsValueCreated)
				l.Value?.Dispose();
		}
	}
}
